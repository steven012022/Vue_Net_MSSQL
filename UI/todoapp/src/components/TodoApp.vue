<template>
  <div>
    <h1>Todo List</h1>

    <!-- Display notes -->
    <ul>
      <li v-for="note in notes" :key="note.id">
        <span>{{ note.id }}: {{ note.description }}</span>
        <button @click="deleteNote(note.id)">Delete</button>
      </li>
    </ul>

    <!-- Add note form -->
    <form @submit.prevent="addNote">
      <input v-model="newNoteDescription" placeholder="Add a note" />
      <button type="submit">Add</button>
    </form>
  </div>
</template>

<script lang="ts">
import { defineComponent, ref, onMounted } from 'vue';
import axios from 'axios';

export default defineComponent({
  name: 'TodoApp',
  setup() {
    // Store the note data
    const notes = ref<Array<{ id: number; description: string }>>([]);
    const newNoteDescription = ref('');

    // Fetch data from the backend
    const fetchNotes = async () => {
      try {
        const response = await axios.get(
          'https://localhost:7242/api/TodoApp/GetNotes',
        );
        notes.value = response.data; // Update the notes data
      } catch (error) {
        console.error('🔥Error fetching notes:', error);
      }
    };

    // Add a new note
    const addNote = async () => {
      if (!newNoteDescription.value) return;

      try {
        // Send a request to add a new note
        await axios.post('https://localhost:7242/api/TodoApp/AddNotes', {
          description: newNoteDescription.value,
        });
        newNoteDescription.value = ''; // Clear the input field
        fetchNotes(); // Reload the notes list
      } catch (error) {
        console.error('🔥Error adding note:', error);
      }
    };

    // Delete a note
    const deleteNote = async (id: number) => {
      try {
        await axios.delete(
          `https://localhost:7242/api/TodoApp/DeleteNotes/${id}`,
        );
        fetchNotes(); // Reload the notes list
      } catch (error) {
        console.error('🔥Error deleting note:', error);
      }
    };

    // Load note data when the component is mounted
    onMounted(() => {
      fetchNotes();
    });

    return {
      notes,
      newNoteDescription,
      fetchNotes,
      addNote,
      deleteNote,
    };
  },
});
</script>

<style scoped>
/* You can add styles here */
</style>
