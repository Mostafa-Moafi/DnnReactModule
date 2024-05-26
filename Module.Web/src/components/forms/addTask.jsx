import { Stack, TextField } from "@mui/material";
import React, { useState } from "react";

export default function AddTask({ form, setForm }) {

    return (
        <Stack sx={{py: 1.5}} spacing={2} >
            <TextField
                id="title-form"
                label="Title"
                value={form.title}
                onChange={(e) => setForm({...form, title: e.target.value})}
                size="small"
                sx={{width: "100%"}}
            />
            <TextField
                id="description"
                label="Description"
                value={form.desc}
                onChange={(e) => setForm({...form, desc: e.target.value})}
                size="small"
                multiline
                minRows={3}
                sx={{width: "100%"}}
            />
        </Stack>
    )
}