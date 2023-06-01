using ailab5gen;
using System;

class Program
{
    static void Main(string[] args)
    {
        // Список вчителів і їх доступних предметів
        List<Teacher> teachers = new List<Teacher>()
        {
        new Teacher("Teacher1", new List<string>{"Math", "Language", "Biology"}),
        new Teacher("Teacher2", new List<string>{"PE", "History"}),
        new Teacher("Teacher3", new List<string>{ "Language", "FLanguage"}),
        new Teacher("Teacher4", new List<string>{ "FLanguage", "Biology", "Math"})
        };
        // Список предметів
        List<string> subjects = new List<string>() { "Math", "PE", "Language", "FLanguage", "Biology", "History", "Chemistry" };

        int numClasses = 2; // Кількість класів
        int daysPerWeek = 5; // Кількість днів в тижні
        int periodsPerDay = 7; // Кількість уроків на день
        int populationSize = 50; // Розмір популяції
        double mutationRate = 0.1; // Ймовірність мутації
        int eliteSize = 5; // Розмір елітної частини популяції

        GeneticAlgorithm geneticAlgorithm = new GeneticAlgorithm(teachers, subjects, numClasses, daysPerWeek, periodsPerDay, populationSize, mutationRate, eliteSize);

        int generations = 100; // Кількість поколінь для оптимізації

        Schedule bestSchedule = geneticAlgorithm.OptimizeSchedule(generations);

        Console.WriteLine("Best Schedule:");
        foreach (var kvp in bestSchedule.ClassSchedule)
        {
            int key = kvp.Key;
            string value = kvp.Value;
            int day = key / periodsPerDay;
            int period = key % periodsPerDay;
            Console.WriteLine($"Day {day + 1}, Period {period + 1}: {value}");
        }
        Console.WriteLine("Fitness: " + geneticAlgorithm.EvaluateSchedule(bestSchedule));

        Console.ReadLine();
    }
}