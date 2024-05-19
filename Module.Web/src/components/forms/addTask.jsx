import { Stack, TextField } from "@mui/material";
import React, { useState } from "react";

export default function AddTask() {

    const [title, setTitle] = useState("")
    const [description, setDescription] = useState("")

    return (
        <Stack sx={{py: 1.5}} spacing={2}>
            <TextField
                id="title-form"
                label="Title"
                value={title}
                onChange={(e) => setTitle(e.target.value)}
                size="small"
                sx={{width: "100%"}}
            />
            <TextField
                id="description"
                label="Description"
                value={description}
                onChange={(e) => setDescription(e.target.value)}
                size="small"
                multiline
                minRows={3}
                sx={{width: "100%"}}
            />
        </Stack>
    )
}