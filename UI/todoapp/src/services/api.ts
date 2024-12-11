import axios from 'axios';

// Define the type for note data
export interface Note {
  id: number;
  content: string;
}

const API_BASE_URL = 'https://localhost:7242/api/TodoApp';

const api = axios.create({
  baseURL: API_BASE_URL,
});

// Get all notes
export const getNotes = async (): Promise<Note[]> => {
  const response = await api.get<Note[]>('/GetNotes');
  return response.data;
};

// Add a new note
export const addNote = async (note: Omit<Note, 'id'>): Promise<Note> => {
  const response = await api.post<Note>('/AddNotes', note);
  return response.data;
};

// Delete a note
export const deleteNote = async (id: number): Promise<void> => {
  await api.delete(`/DeleteNotes/${id}`);
};
