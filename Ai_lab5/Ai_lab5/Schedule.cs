using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ai_lab5
{
    class Schedule
    {
        // Define the genetic representation of the schedule
        bool[] genes;
        private List<List<Subject>> CreateSchedule(Subject[] subjects, Random random)
        {
            List<List<Subject>> schedule = new List<List<Subject>>();
            for(int i = 0; i < 5; i++)
            {
                List<Subject> classDay = new List<Subject>();
                int rand = random.Next(4, 6);
                Console.WriteLine(rand);
                for (int j = 0; j < 5; j++)
                {
                    classDay.Add(subjects[random.Next(0, subjects.Length - 1)]);
                }
                schedule.Add(classDay);
            }
            return schedule;
        }
        public List<List<List<Subject>>> CreatePopulation(int n, Subject[] subjects, Random random)
        {
            List<List<List<Subject>>> population = new List<List<List<Subject>>>();
            for(int i = 0; i < n;i++)
            {
                population.Add(CreateSchedule(subjects , random));
            }
            return population;
        }

        // Fitness function to evaluate the schedule
        public int Fitness(List<List<Subject>> schedule)
        {
            return 1;
        }

        // Crossover operation
        public Schedule Crossover(Schedule other)
        {
            return null;
        }

        // Mutation operation
        public void Mutate()
        {
            // Introduce random changes to the genes of the schedule
        }
        public void PrintSchedule(List<List<List<Subject>>> population)
        {
            for(int i = 0; i < population.Count; i++)
            {
                Console.WriteLine("Population number: " + i);
                for(int j = 0; j < 5; j++)
                {
                    Console.WriteLine("Day number: " + j);
                    for (int k = 0; k < population[i][j].Count; k++)
                    {
                        PrintSubject(population[i][j][k]);
                    }
                }
                Console.WriteLine("llllllllllllllllllllllllll");
            }
            
        }
        public void PrintSubject(Subject subject)
        {
            Console.WriteLine(subject.Name + " " + subject.Teacher);
        }
    }
}
