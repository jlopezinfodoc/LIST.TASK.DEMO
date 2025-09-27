namespace LIST.TASK.DEMO.Servicios.Exceptions
{
    public class TaskNotFoundException : Exception
    {
        public TaskNotFoundException(int taskId) 
            : base($"Tarea con ID {taskId} no fue encontrada")
        {
        }

        public TaskNotFoundException(string message) 
            : base(message)
        {
        }

        public TaskNotFoundException(string message, Exception innerException) 
            : base(message, innerException)
        {
        }
    }

    public class TaskValidationException : Exception
    {
        public TaskValidationException(string message) 
            : base(message)
        {
        }

        public TaskValidationException(string message, Exception innerException) 
            : base(message, innerException)
        {
        }
    }

    public class TaskAlreadyCompletedException : Exception
    {
        public TaskAlreadyCompletedException(int taskId) 
            : base($"La tarea con ID {taskId} ya está completada")
        {
        }

        public TaskAlreadyCompletedException(string message) 
            : base(message)
        {
        }
    }
}