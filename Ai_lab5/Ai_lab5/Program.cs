
using Ai_lab5;

Schedule schedule = new Schedule();
Subject[] subjects = new Subject[8] {new Subject("math", "maria ivanivna", 4), new Subject("pe", "petro andriyovych", 3), new Subject("ukr language", "stefa ruslanivna", 4),
                                     new Subject("bioligy", "andriy nazarovych", 2), new Subject("history", "vasyl` levovych", 3), new Subject("eng language", "olga andriivna", 3),
                                     new Subject("art", "Maria stepanivna", 2), new Subject("literature", "galyna olegivna", 4)};
Random random = new Random();
List<List<List<Subject>>> population = schedule.CreatePopulation(10, subjects, random);
schedule.PrintSchedule(population);


