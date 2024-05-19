import { useState } from "react"

export default function useAddTodo() {

    const [list, setList] = useState([
        {
            id: 1,
            title: "testy todo list my todo number 1",
            timeAdded: "2024/5/10"
        },
        {
            id: 2,
            title: "testy todo list todo number 2",
            timeAdded: "2025/5/23"
        },
        {
            id: 3,
            title: "testy todo list shopping",
            timeAdded: "2024/5/10"
        },
        {
            id: 4,
            title: "testy todo list",
            timeAdded: "2024/5/10"
        },
    ])

    const add = (data) => {
        setList([...list, data])
    }

    const edit = () => {
        console.log("eddited");
    }

    const remove = (data) => {
        const newData = list.filter((e) => e.id != data.id) 
        setList(newData)
    }

    return [{list}, {add, edit, remove}]
}