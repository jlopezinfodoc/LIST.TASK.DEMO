# Integración con Angular - Ejemplos de Código

## Servicio Angular para consumir la API

### 1. **Instalar HttpClient** (si no está instalado)

En `app.module.ts` (Angular < 17) o `main.ts` (Angular 17+):

```typescript
// Angular < 17 (app.module.ts)
import { HttpClientModule } from '@angular/common/http';

@NgModule({
  imports: [
    BrowserModule,
    HttpClientModule  // ← Agregar esto
  ],
  // ...
})

// Angular 17+ (main.ts)
import { bootstrapApplication } from '@angular/platform-browser';
import { provideHttpClient } from '@angular/common/http';

bootstrapApplication(AppComponent, {
  providers: [
    provideHttpClient(), // ← Agregar esto
    // otros providers...
  ]
});
```

### 2. **Modelo TypeScript para Task**

```typescript
// models/task.model.ts
export interface Task {
  id: number;
  title: string;
  description?: string;
  isCompleted: boolean;
  createdDate: string;
  completedDate?: string;
}

export interface CreateTaskDto {
  title: string;
  description?: string;
}

export interface UpdateTaskDto {
  title: string;
  description?: string;
  isCompleted: boolean;
}

export interface ApiResponse<T> {
  data: T;
  statusCode: number;
  message: string;
  success: boolean;
}

export interface TaskFilter {
  isCompleted?: boolean;
  title?: string;
  createdFrom?: string;
  createdTo?: string;
  pageNumber?: number;
  pageSize?: number;
}
```

### 3. **Servicio Angular**

```typescript
// services/task.service.ts
import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Task, CreateTaskDto, UpdateTaskDto, ApiResponse, TaskFilter } from '../models/task.model';

@Injectable({
  providedIn: 'root'
})
export class TaskService {
  private apiUrl = 'http://localhost:5052/api/tasks'; // ← URL de tu API

  constructor(private http: HttpClient) {}

  // GET /api/tasks - Obtener todas las tareas
  getTasks(filter?: TaskFilter): Observable<ApiResponse<Task[]>> {
    let params = new HttpParams();
    
    if (filter) {
      if (filter.isCompleted !== undefined) {
        params = params.set('isCompleted', filter.isCompleted.toString());
      }
      if (filter.title) {
        params = params.set('title', filter.title);
      }
      if (filter.createdFrom) {
        params = params.set('createdFrom', filter.createdFrom);
      }
      if (filter.createdTo) {
        params = params.set('createdTo', filter.createdTo);
      }
      if (filter.pageNumber) {
        params = params.set('pageNumber', filter.pageNumber.toString());
      }
      if (filter.pageSize) {
        params = params.set('pageSize', filter.pageSize.toString());
      }
    }

    return this.http.get<ApiResponse<Task[]>>(this.apiUrl, { params });
  }

  // GET /api/tasks/{id} - Obtener tarea por ID
  getTaskById(id: number): Observable<ApiResponse<Task>> {
    return this.http.get<ApiResponse<Task>>(\`\${this.apiUrl}/\${id}\`);
  }

  // POST /api/tasks - Crear nueva tarea
  createTask(task: CreateTaskDto): Observable<ApiResponse<{id: number, message: string}>> {
    return this.http.post<ApiResponse<{id: number, message: string}>>(this.apiUrl, task);
  }

  // PUT /api/tasks/{id} - Actualizar tarea
  updateTask(id: number, task: UpdateTaskDto): Observable<ApiResponse<Task>> {
    return this.http.put<ApiResponse<Task>>(\`\${this.apiUrl}/\${id}\`, task);
  }

  // PATCH /api/tasks/{id}/complete - Marcar como completada
  completeTask(id: number): Observable<ApiResponse<Task>> {
    return this.http.patch<ApiResponse<Task>>(\`\${this.apiUrl}/\${id}/complete\`, {});
  }

  // DELETE /api/tasks/{id} - Eliminar tarea
  deleteTask(id: number): Observable<ApiResponse<boolean>> {
    return this.http.delete<ApiResponse<boolean>>(\`\${this.apiUrl}/\${id}\`);
  }
}
```

### 4. **Componente Angular**

```typescript
// components/task-list.component.ts
import { Component, OnInit } from '@angular/core';
import { TaskService } from '../services/task.service';
import { Task, CreateTaskDto } from '../models/task.model';

@Component({
  selector: 'app-task-list',
  template: \`
    <div class="task-container">
      <h2>Lista de Tareas</h2>
      
      <!-- Formulario para crear tarea -->
      <div class="create-task">
        <input 
          [(ngModel)]="newTask.title" 
          placeholder="Título de la tarea"
          class="form-control">
        <textarea 
          [(ngModel)]="newTask.description" 
          placeholder="Descripción (opcional)"
          class="form-control"></textarea>
        <button (click)="createTask()" class="btn btn-primary">
          Crear Tarea
        </button>
      </div>

      <!-- Filtros -->
      <div class="filters">
        <label>
          <input type="checkbox" [(ngModel)]="showCompleted"> 
          Mostrar completadas
        </label>
        <input 
          [(ngModel)]="titleFilter" 
          placeholder="Buscar por título..."
          (input)="applyFilters()">
        <button (click)="loadTasks()" class="btn btn-secondary">
          Actualizar
        </button>
      </div>

      <!-- Lista de tareas -->
      <div class="task-list">
        <div *ngFor="let task of tasks" class="task-item" 
             [class.completed]="task.isCompleted">
          <h4>{{ task.title }}</h4>
          <p>{{ task.description }}</p>
          <p><small>Creada: {{ task.createdDate | date:'short' }}</small></p>
          <p *ngIf="task.completedDate">
            <small>Completada: {{ task.completedDate | date:'short' }}</small>
          </p>
          
          <div class="task-actions">
            <button 
              *ngIf="!task.isCompleted" 
              (click)="completeTask(task.id)"
              class="btn btn-success">
              Completar
            </button>
            <button 
              (click)="deleteTask(task.id)"
              class="btn btn-danger">
              Eliminar
            </button>
          </div>
        </div>
      </div>

      <!-- Estado de carga -->
      <div *ngIf="loading" class="loading">
        Cargando tareas...
      </div>

      <!-- Mensaje de error -->
      <div *ngIf="error" class="alert alert-danger">
        {{ error }}
      </div>
    </div>
  \`,
  styles: [\`
    .task-container { padding: 20px; }
    .create-task { margin-bottom: 20px; }
    .create-task input, .create-task textarea { 
      margin-bottom: 10px; 
      width: 100%; 
      padding: 8px; 
    }
    .filters { margin-bottom: 20px; }
    .filters input { margin-right: 10px; }
    .task-item { 
      border: 1px solid #ddd; 
      padding: 15px; 
      margin-bottom: 10px; 
      border-radius: 5px; 
    }
    .task-item.completed { 
      background-color: #f0f8f0; 
      opacity: 0.7; 
    }
    .task-actions button { 
      margin-right: 10px; 
    }
    .btn { 
      padding: 5px 10px; 
      border: none; 
      border-radius: 3px; 
      cursor: pointer; 
    }
    .btn-primary { background-color: #007bff; color: white; }
    .btn-success { background-color: #28a745; color: white; }
    .btn-danger { background-color: #dc3545; color: white; }
    .btn-secondary { background-color: #6c757d; color: white; }
    .loading { text-align: center; padding: 20px; }
    .alert-danger { 
      background-color: #f8d7da; 
      border: 1px solid #f5c6cb; 
      color: #721c24; 
      padding: 10px; 
      border-radius: 5px; 
    }
  \`]
})
export class TaskListComponent implements OnInit {
  tasks: Task[] = [];
  loading = false;
  error: string | null = null;
  
  // Formulario para nueva tarea
  newTask: CreateTaskDto = {
    title: '',
    description: ''
  };
  
  // Filtros
  showCompleted = false;
  titleFilter = '';

  constructor(private taskService: TaskService) {}

  ngOnInit() {
    this.loadTasks();
  }

  loadTasks() {
    this.loading = true;
    this.error = null;
    
    const filter = {
      isCompleted: this.showCompleted || undefined,
      title: this.titleFilter || undefined,
      pageNumber: 1,
      pageSize: 50
    };

    this.taskService.getTasks(filter).subscribe({
      next: (response) => {
        if (response.success) {
          this.tasks = response.data;
        } else {
          this.error = response.message;
        }
        this.loading = false;
      },
      error: (err) => {
        this.error = 'Error al cargar las tareas: ' + err.message;
        this.loading = false;
        console.error('Error:', err);
      }
    });
  }

  createTask() {
    if (!this.newTask.title.trim()) {
      this.error = 'El título es obligatorio';
      return;
    }

    this.taskService.createTask(this.newTask).subscribe({
      next: (response) => {
        if (response.success) {
          this.newTask = { title: '', description: '' };
          this.loadTasks(); // Recargar la lista
        } else {
          this.error = response.message;
        }
      },
      error: (err) => {
        this.error = 'Error al crear la tarea: ' + err.message;
        console.error('Error:', err);
      }
    });
  }

  completeTask(id: number) {
    this.taskService.completeTask(id).subscribe({
      next: (response) => {
        if (response.success) {
          this.loadTasks(); // Recargar la lista
        } else {
          this.error = response.message;
        }
      },
      error: (err) => {
        this.error = 'Error al completar la tarea: ' + err.message;
        console.error('Error:', err);
      }
    });
  }

  deleteTask(id: number) {
    if (confirm('¿Estás seguro de que quieres eliminar esta tarea?')) {
      this.taskService.deleteTask(id).subscribe({
        next: (response) => {
          if (response.success) {
            this.loadTasks(); // Recargar la lista
          } else {
            this.error = response.message;
          }
        },
        error: (err) => {
          this.error = 'Error al eliminar la tarea: ' + err.message;
          console.error('Error:', err);
        }
      });
    }
  }

  applyFilters() {
    this.loadTasks();
  }
}
```

### 5. **Configuración en app.module.ts** (si usas módulos)

```typescript
// app.module.ts
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms'; // Para ngModel

import { AppComponent } from './app.component';
import { TaskListComponent } from './components/task-list.component';

@NgModule({
  declarations: [
    AppComponent,
    TaskListComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule, // ← Para HTTP requests
    FormsModule       // ← Para ngModel
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
```

## ✅ Pasos para Probar

### 1. **Verificar que la API esté corriendo:**
```bash
# Tu API debería estar en:
http://localhost:5052/api/tasks
```

### 2. **Verificar CORS desde Angular:**
Abre las herramientas de desarrollador (F12) y verifica que no hay errores de CORS.

### 3. **Probar un endpoint simple:**
```typescript
// En cualquier componente, en ngOnInit:
this.taskService.getTasks().subscribe({
  next: (response) => console.log('✅ API funcionando:', response),
  error: (err) => console.error('❌ Error CORS:', err)
});
```

### 4. **Ejemplo de uso rápido:**
```typescript
// Crear una tarea
const newTask = { title: 'Mi primera tarea', description: 'Descripción' };
this.taskService.createTask(newTask).subscribe(response => {
  console.log('Tarea creada:', response);
});

// Obtener todas las tareas
this.taskService.getTasks().subscribe(response => {
  console.log('Tareas:', response.data);
});
```

## 🔧 Troubleshooting

### Si sigues teniendo problemas de CORS:

1. **Verifica la URL exacta** en el servicio Angular
2. **Abre las DevTools** y mira los errores en la consola
3. **Verifica que la API esté corriendo** en el puerto correcto
4. **Prueba desde Postman** primero para asegurar que la API funciona
5. **Limpia la caché** del navegador (Ctrl+Shift+R)

¡Con esta configuración tu aplicación Angular debería funcionar perfectamente con la API!