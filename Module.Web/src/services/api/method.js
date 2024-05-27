import axios from 'axios'

// axios.defaults.baseURL = process.env.REACT_APP_BASE_URL

const authHead = {
	"Tabid": document.getElementById("root").getAttribute("tab-id"),
	"Moduleid": document.getElementById("root").getAttribute("module-id"),
	"__RequestVerificationToken": document.querySelector(`input[name="__RequestVerificationToken"]`).value,
}

export async function get(url) {
    const config = {
        headers: {
            ...authHead
        }
    }

    return await axios.get(url, config)
        .then(resposne => resposne.data)
        .catch(error => {console.log(error)})
}

export async function post(url, data) {

    const config = {
        headers: {
            "Content-Type": "application/json",
            ...authHead
        }
    }

    return await axios.post(url, data, config)
        .then(resposne => resposne.data)
        .catch(error => {console.log(error)})
}

export async function deleteMethod(url) {

    const config = {
        headers: {
            "Content-Type": "application/json",
            ...authHead
        }
    }

    return await axios.delete(url, config)
        .then(resposne => resposne.data)
        .catch(error => {console.log(error)})
}

export async function put(url, body) {

    const config = {
        headers: {
            "Content-Type": "application/json",
            ...authHead
        }
    }

    return await axios.put(url, body, config)
        .then(resposne => resposne.data)
        .catch(error => {console.log(error)})
}
