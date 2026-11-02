using System;
using System.Collections.Generic;
using StudentRegistryApp.Events;
using StudentRegistryApp.Models;

namespace StudentRegistryApp.Services
{
    public class DeanOfficeNotifier
    {
        private readonly HashSet<Student> warnedStudents = new HashSet<Student>();
        public event Action<string>? WarningCreated;
        public void OnMarksChanged(object? sender, MarksChangedEventArgs e)
        {
            if (sender is not Student student)
            {
                return;
            }

            if (student.Marks.Count == 0)
            {
                warnedStudents.Remove(student);
                return;
            }

            if (e.NewAverage >= 3)
            {
                warnedStudents.Remove(student);
                return;
            }

            if (warnedStudents.Contains(student))
            {
                return;
            }

            warnedStudents.Add(student);

            string message = $"Попередження! Студент ID {e.StudentId} має середній бал {e.NewAverage:F2}.";
            WarningCreated?.Invoke(message);
        }

        public void Reset()
        {
            warnedStudents.Clear();
        }

        public void RemoveStudent(Student student)
        {
            warnedStudents.Remove(student);
        }
    }
}