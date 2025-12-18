using System;
// Práctica de Estructura de Datos
// Agenda de turnos de pacientes – Programación Orientada a Objetos en C#

namespace AgendaClinica
{
    // Definición de la Clase (POO)
    public class Paciente
    {
        // Atributos del paciente
        public string Nombre { get; set; }
        public string Cedula { get; set; }
        public string Motivo { get; set; }

        // Constructor de la clase
        public Paciente(string nombre, string cedula, string motivo)
        {
            Nombre = nombre;
            Cedula = cedula;
            Motivo = motivo;
        }

        public override string ToString()
        {
            return $"{Nombre} (ID: {Cedula}) - {Motivo}";
        }
    }

    class Program
    {
        // Vectores (Arrays unidimensionales) para días y horas
        static string[] dias = { "Lunes", "Martes", "Miércoles", "Jueves", "Viernes" };
        static string[] horas = { "08:00", "09:00", "10:00", "11:00", "12:00" };

        // MATRIZ (Array bidimensional): [Días, Horas]
        static Paciente[,] agenda = new Paciente[5, 5];

        static void Main(string[] args)
        {
            bool continuar = true;
            while (continuar)
            {
                Console.WriteLine("\n=== SISTEMA DE GESTIÓN DE TURNOS - CLÍNICA UEA ===");
                Console.WriteLine("1. Agendar Turno");
                Console.WriteLine("2. Ver Agenda Completa (Reportería)");
                Console.WriteLine("3. Salir");
                Console.Write("Seleccione una opción: ");
                
                string opcion = Console.ReadLine()!;

                // Selección de acción según opción ingresada
                switch (opcion)
                {
                    case "1":
                        AgendarTurno();
                        break;
                    case "2":
                        VerAgenda();
                        break;
                    case "3":
                        continuar = false;
                        break;
                    default:
                        Console.WriteLine("Opción no válida.");
                        break;
                }
            }
        }

        // Método para agendar un turno
        static void AgendarTurno()
        {
            Console.WriteLine("\n--- AGENDAR TURNO ---");
            Console.Write("Nombre del paciente: ");
            string nombre = Console.ReadLine()!;
            string cedula = "";
            while (true) // Validación de cédula
            {
                Console.Write("Ingrese la cédula (10 dígitos): ");
                cedula = Console.ReadLine()!;
                if (cedula.Length == 10 && long.TryParse(cedula, out _)) break;
                Console.WriteLine("Error: La cédula debe ser numérica y tener 10 dígitos.");
            }
            Console.Write("Motivo de consulta: ");
            string motivo = Console.ReadLine()!;

            Paciente nuevoPaciente = new Paciente(nombre, cedula, motivo);

            // Mostrar Días disponibles
            Console.WriteLine("Días disponibles:");
            for (int i = 0; i < dias.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {dias[i]}");
            }
            // Selección y validación del día
            int fila;
            while (true)
            {
                Console.Write("Seleccione el número del día: ");
                if (int.TryParse(Console.ReadLine(), out int diaSeleccionado) &&
                    diaSeleccionado >= 1 && diaSeleccionado <= dias.Length)
                {
                    fila = diaSeleccionado - 1;
                    break;
                }
                Console.WriteLine("Día inválido. Intente nuevamente.");
            }

                // Mostrar Horas disponibles
                Console.WriteLine($"Horas disponibles para {dias[fila]}:");
                for (int j = 0; j < horas.Length; j++)
                {
                    string estado = (agenda[fila, j] != null) ? "Ocupado" : "Libre";
                    Console.WriteLine($"{j + 1}. {horas[j]} - [{estado}]");
                }
                // Selección y validación de la hora
                int col;
                while (true)
                {
                Console.Write("Seleccione el número de la hora: ");
                if (int.TryParse(Console.ReadLine(), out int horaSeleccionada) && horaSeleccionada >= 1 && horaSeleccionada <= 5)
                {
                    col = horaSeleccionada - 1;

                    // Verificar si el turno está libre
                    if (agenda[fila, col] == null)
                    {
                        agenda[fila, col] = nuevoPaciente;
                        Console.WriteLine($"¡Turno agendado exitosamente para el {dias[fila]} a las {horas[col]}!");
                        break;
                    }
                    else
                    {
                        Console.WriteLine("¡Error! Ese turno ya está ocupado.");
                    }
                }
                else
                {
                    Console.WriteLine("Hora inválida. Intente nuevamente.");
                }
            }
        }

        // Método para ver la agenda completa (reportería)
        static void VerAgenda()
        {
            Console.WriteLine("\n--- AGENDA SEMANAL ---");
            for (int i = 0; i < dias.Length; i++) // Recorre filas (Días)
            {
                Console.WriteLine($"\nDIA: {dias[i]}");
                for (int j = 0; j < horas.Length; j++) // Recorre columnas (Horas)
                {
                    Paciente p = agenda[i, j];
                    if (p != null)
                    {
                        Console.WriteLine($"  {horas[j]}: [OCUPADO] {p.ToString()}");
                    }
                    else
                    {
                        Console.WriteLine($"  {horas[j]}: [LIBRE]");
                    }
                }
            }
        }
    }
}