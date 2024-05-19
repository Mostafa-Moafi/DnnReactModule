import { Stack, styled, FormControlLabel, Checkbox, Typography, IconButton } from "@mui/material";
import React, { useState } from "react";
import EditIcon from '@mui/icons-material/Edit';
import DeleteIcon from '@mui/icons-material/Delete';

export default function TodoItem({ data }) {

    const [selected, setSelected] = useState(false)

    const select = (e, value) => {
        setSelected(value)
    }

    return (
        <Item>
            <Stack direction="row" alignItems="center" spacing={1}>
                <Checkbox
                    value={selected}
                    checked={selected}
                    onChange={select}
                    color="primary"
                    size="small"
                />
                <Stack direction="row" sx={{flex: 1}} justifyContent="space-between" alignItems="center">
                    <Stack>
                        <Typography variant="body1" color="initial">{data.title}</Typography>
                        <Typography variant="caption" color="initial">{data.timeAdded}</Typography>
                    </Stack>
                    <Stack direction="row" alignItems="center" >
                        <IconButton>
                            <DeleteIcon />
                        </IconButton>
                        <IconButton>
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
