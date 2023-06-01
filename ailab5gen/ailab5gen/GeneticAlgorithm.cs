using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ailab5gen
{
    class GeneticAlgorithm
    {
        private List<Teacher> teachers;
        private List<string> subjects;
        private int numClasses;
        private int daysPerWeek;
        private int periodsPerDay;
        private int populationSize;
        private double mutationRate;
        private int eliteSize;

        public GeneticAlgorithm(List<Teacher> teachers, List<string> subjects, int numClasses, int daysPerWeek, int periodsPerDay, int populationSize, double mutationRate, int eliteSize)
        {
            this.teachers = teachers;
            this.subjects = subjects;
            this.numClasses = numClasses;
            this.daysPerWeek = daysPerWeek;
            this.periodsPerDay = periodsPerDay;
            this.populationSize = populationSize;
            this.mutationRate = mutationRate;
            this.eliteSize = eliteSize;
        }

        public Schedule OptimizeSchedule(int generations)
        {
            List<Schedule> population = InitializePopulation();

            for (int i = 0; i < generations; i++)
            {
                population = NextGeneration(population);
            }

            return GetBestSchedule(population);
        }

        private List<Schedule> InitializePopulation()
        {
            List<Schedule> population = new List<Schedule>();

            for (int i = 0; i < populationSize; i++)
            {
                Schedule schedule = new Schedule();

                for (int day = 0; day < daysPerWeek; day++)
                {
                    for (int period = 0; period < periodsPerDay; period++)
                    {
                        Random randomNum = new Random();
                        int classIndex = randomNum.Next(0, numClasses);
                        int teacherIndex = randomNum.Next(0, teachers.Count);
                        string subject = teachers[teacherIndex].AvailableSubjects[randomNum.Next(0, teachers[teacherIndex].AvailableSubjects.Count)];
                        int key = day * periodsPerDay + period;
                        schedule.ClassSchedule.Add(key, $"{subject} - Class {classIndex + 1}");
                    }
                }

                population.Add(schedule);
            }

            return population;
        }

        private List<Schedule> NextGeneration(List<Schedule> population)
        {
            List<Schedule> newPopulation = new List<Schedule>();

            List<Schedule> elites = GetElites(population);

            while (newPopulation.Count < populationSize)
            {
                Schedule parent1 = SelectParent(population);
                Schedule parent2 = SelectParent(population);
                Schedule offspring = Crossover(parent1, parent2);
                Mutate(offspring);

                newPopulation.Add(offspring);
            }

            newPopulation.AddRange(elites);

            return newPopulation;
        }

        private List<Schedule> GetElites(List<Schedule> population)
        {
            return population.OrderBy(schedule => EvaluateSchedule(schedule)).Take(eliteSize).ToList();
        }

        private Schedule SelectParent(List<Schedule> population)
        {
            List<Schedule> tournament = new List<Schedule>();

            for (int i = 0; i < 2; i++)
            {
                Random randomNum = new Random();
                int randomIndex = randomNum.Next(0, population.Count);
                tournament.Add(population[randomIndex]);
            }

            return tournament.OrderBy(schedule => EvaluateSchedule(schedule)).First();
        }

        private Schedule Crossover(Schedule parent1, Schedule parent2)
        {
            Schedule offspring = new Schedule();

            foreach (var kvp in parent1.ClassSchedule)
            {
                int key = kvp.Key;
                string value = kvp.Value;

                offspring.ClassSchedule[key] = value;
            }

            foreach (var kvp in parent2.ClassSchedule)
            {
                int key = kvp.Key;
                string value = kvp.Value;

                if (!offspring.ClassSchedule.ContainsKey(key))
                    offspring.ClassSchedule[key] = value;
            }

            return offspring;
        }

        private void Mutate(Schedule schedule)
        {
            foreach (var kvp in schedule.ClassSchedule)
            {
                int key = kvp.Key;

                Random randomNum = new Random();
                if (randomNum.NextDouble() < mutationRate)
                {
                    int classIndex = randomNum.Next(0, numClasses);
                    int teacherIndex = randomNum.Next(0, teachers.Count);
                    string subject = teachers[teacherIndex].AvailableSubjects[randomNum.Next(0, teachers[teacherIndex].AvailableSubjects.Count)];
                    schedule.ClassSchedule[key] = $"{subject} - Class {classIndex + 1}";
                }
            }
        }

        public double EvaluateSchedule(Schedule schedule)
        {
            double fitness = 0.0;

            // Перевірка повторення предметів у один день
            for (int day = 0; day < daysPerWeek; day++)
            {
                List<string> subjectsOfDay = new List<string>();
                foreach (var kvp in schedule.ClassSchedule)
                {
                    int key = kvp.Key;
                    string value = kvp.Value;
                    if (key / periodsPerDay == day)
                    {
                        string subject = value.Split('-')[0].Trim();
                        if (subjectsOfDay.Contains(subject))
                        {
                            // Якщо предмет повторюється, збільшуємо значення фітнесу
                            fitness += 1.0;
                        }
                        else
                        {
                            subjectsOfDay.Add(subject);
                        }
                    }
                }
            }

            // Перевірка наявності вчителів на двох уроках у двох класах
            for (int period = 0; period < periodsPerDay; period++)
            {
                for (int classIndex = 0; classIndex < numClasses; classIndex++)
                {
                    bool teacherPresent = false;
                    foreach (var kvp in schedule.ClassSchedule)
                    {
                        int key = kvp.Key;
                        string value = kvp.Value;
                        if (key % periodsPerDay == period && value.Contains($"Class {classIndex + 1}"))
                        {
                            string subject = value.Split('-')[0].Trim();
                            string teacher = value.Split('-')[1].Trim();
                            if (teachers.Any(t => t.Name == teacher && t.AvailableSubjects.Contains(subject)))
                            {
                                teacherPresent = true;
                                break;
                            }
                        }
                    }

                    if (!teacherPresent)
                    {
                        // Якщо вчитель не присутній на уроці, збільшуємо значення фітнесу
                        fitness += 1.0;
                    }
                }
            }
            Console.WriteLine(fitness);
            return fitness;
        }

        private Schedule GetBestSchedule(List<Schedule> population)
        {
            return population.OrderBy(schedule => EvaluateSchedule(schedule)).First();
        }
    }
}
