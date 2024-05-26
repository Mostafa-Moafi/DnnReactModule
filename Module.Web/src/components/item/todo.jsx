import React, { useState } from "react";
import { Stack, styled, FormControlLabel, Checkbox, Typography, IconButton } from "@mui/material";
import EditIcon from '@mui/icons-material/Edit';
import DeleteIcon from '@mui/icons-material/Delete';

export default function TodoItem({ data, remove, handleModal, setForm }) {

    return (
        <Item>
            <Stack direction="row" alignItems="center" spacing={1}>
                <Stack direction="row" sx={{flex: 1}} justifyContent="space-between" alignItems="center">
                    <Stack>
                        <Typography variant="body1" color="initial">{data.Description}</Typography>
                        <Typography variant="caption" color="initial">{data.Title}</Typography>
                    </Stack>
                    <Stack direction="row" alignItems="center" >
                        <IconButton onClick={() => remove(data.Id)}>
                            <DeleteIcon />
                        </IconButton>
                        <IconButton onClick={() => {setForm({desc: data.Description, title: data.Title, id: data.Id}); handleModal(true)}}>
                            <EditIcon />
                        </IconButton>
                    </Stack>
                </Stack>
            </Stack>
        </Item>
    )
}

const Item = styled(Stack)(
	({ theme }) => `
		background: white;
		border-radius: 4px;
		padding: ${theme.spacing(1)} ${theme.spacing(1.5)};
	`
)
