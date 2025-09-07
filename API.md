
## 📖 API Documentation (Swagger)

L'API è completamente documentata tramite **Swagger UI**.  
Una volta avviato il progetto, la documentazione interattiva è disponibile qui:  

👉 [http://localhost:5005/swagger/index.html](http://localhost:5005/swagger/index.html)  

### Endpoints principali

#### 🔹 ToDoLists
- **GET /api/ToDoLists** → Ritorna tutte le liste.  
- **GET /api/ToDoLists/{id}** → Ritorna una singola lista con le sue attività.  
- **POST /api/ToDoLists** → Crea una nuova lista.  
- **PUT /api/ToDoLists/{id}** → Aggiorna una lista esistente.  
- **DELETE /api/ToDoLists/{id}** → Soft delete di una lista.  

#### 🔹 TaskActivities
- **GET /api/TaskActivity** → Ritorna tutte le attività.  
- **GET /api/TaskActivity/{id}** → Ritorna una singola attività.  
- **POST /api/TaskActivity** → Crea una nuova attività.  
- **PUT /api/TaskActivity/{id}** → Aggiorna un’attività esistente.  
- **DELETE /api/TaskActivity/{id}** → Soft delete di un’attività.  


