import { useEffect, useState } from "react"
import { deleteMethod, get, post, put } from "../api/method"
import { ApiUrls } from "../api/urls"

export default function useAddTodo(handleModal) {

    const [list, setList] = useState([])
    const [form, setForm] = useState({title: "", desc: "", id: null})

    const addOrEdit = () => {
        const bodyObj = {
            "Title": form.title,
            "Description": form.desc
        }
        if(form.id) {
            put(`${ApiUrls.Update}?id=${form.id}`, bodyObj).then(() => reset())
        } else {
            post(ApiUrls.Add, bodyObj).then(() => reset())
        }
    }

    const edit = () => {
        handleModal(true)
        console.log("eddited");
    }

    const remove = (id) => {
        deleteMethod(`${ApiUrls.Delete}?id=${id}`).then((res) => {
            getList()
        })
    }

    const getList = () => {
		get(ApiUrls.Get).then((res) => {
            setList(res.Data)
        })
    }

    const reset = () => {
        handleModal(false)
        getList()
        setForm({title: "", desc: "", id: null})
    }

    useEffect(() => {
        getList()
	}, [])

    return [{list, form}, {addOrEdit, edit, remove, setForm}]
}