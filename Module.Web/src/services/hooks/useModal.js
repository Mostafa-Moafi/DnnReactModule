import { useState } from "react"

export default function useModal() {

    const [open, setOpen] = useState(false)

    const handleModal = (state) => {
        setOpen(state)
    }

    return [{open}, handleModal]
}