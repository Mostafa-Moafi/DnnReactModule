import React from 'react';
import { Stack, Typography, Button, styled, Dialog, DialogTitle, DialogContent, DialogContentText, DialogActions } from "@mui/material"
import useAddTodo from "./services/hooks/useAddTodo"
import TodoItem from "./components/item/todo"
import useModal from "./services/hooks/useModal"
import AddTask from "./components/forms/addTask"

export default function App() {

	const [{open}, handleModal] = useModal()
	const [{list}, {add, edit, remove}] = useAddTodo()

	return (
		<Stack sx={{p: 6}} alignItems="center" spacing={4} >
			<Typography variant="h3" color="initial" sx={{textAlign: "center", color: "#555"}}>TODO LIST</Typography>
			<Stack width={900} spacing={2} >
				<Stack direction="row">
					<Button onClick={() => handleModal(true)} variant="contained" color="primary">
						Add Task
					</Button>
				</Stack>
				<TodoContainer spacing={1.5}>
					{
						list.map((val, i) => {
							return (
								<TodoItem key={i} data={val} />
							)
						})
					}
				</TodoContainer>
				<Dialog 
					PaperProps={{sx: {p: 1}}} 
					open={open} 
					onClose={handleModal} 
				>
				  	<DialogTitle>
						Add new task
				  	</DialogTitle>
				  	<DialogContent sx={{width: "500px"}}>
						<DialogContentText>
							<AddTask />
						</DialogContentText>
				  	</DialogContent>
				  	<DialogActions>
						<Button
						  	onClick={() => handleModal(false)}
						  	color="primary"
							sx={{mr: 1}}
						>
						  	Cancel
						</Button>
						<Button
						  	onClick={() => handleModal(false)}
						  	color="primary"
							variant="contained"
						>
						  	Add
						</Button>
				  	</DialogActions>
				</Dialog>
			</Stack>
		</Stack>
	)
}

const TodoContainer = styled(Stack)(
	({  }) => `
		background: #eaeaea;
		padding: 16px;
		border-radius: 8px;
	`
)
