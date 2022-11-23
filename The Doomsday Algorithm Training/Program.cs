using System;

namespace The_Doomsday_Algorithm_Training
{
    internal class Program
    {
        static void Main(string[] args)
        {
            MainMenu();
        }
        public static void MainMenu()
        {
            Random random = new Random();
            Dictionary<string, int> monthsDaysNotLeapYear = new Dictionary<string, int>()
            {
                {"January",     31 },
                {"February",    28 },
                {"March",       31 },
                {"April",       30 },
                {"May",         31 },
                {"June",        30 },
                {"July",        31 },
                {"August",      31 },
                {"September",   30 },
                {"October",     31 },
                {"November",    30 },
                {"December",    31 }
            };
            Dictionary<string, int> monthsDoomsdays = new Dictionary<string, int>()
            {
                {"January",     3 },
                {"February",    28 },
                {"March",       14 },
                {"April",       4 },
                {"May",         9 },
                {"June",        6 },
                {"July",        11 },
                {"August",      8 },
                {"September",   5 },
                {"October",     10 },
                {"November",    7 },
                {"December",    12 }
            };

            Console.Clear();

            string[] weekDays = new string[] { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };


            Console.WriteLine("Hello to Doomsday Algorithm Training!\nLet's do some training\n\n" +
                "Choose training you need:\n" +
                "1. Doomsday of certain Century training.\n" +
                "2. Doomsday of certain Year\n" +
                "3. Doomsday of certain month training.\n" +
                "4. Weekday of certain date.\n" +
                "To return to menu type \"return\"");

            string trainingMode = Console.ReadLine() ?? "";
            switch (trainingMode)
            {
                case "1":
                    ShowTipAndClearConsole();
                    CenturyDoomsday(random, weekDays);
                    break;
                case "2":
                    ShowTipAndClearConsole();
                    YearDoomsday(random, monthsDoomsdays, monthsDaysNotLeapYear, weekDays);
                    break;
                case "3":
                    ShowTipAndClearConsole();
                    MonthDoomsday(random, monthsDoomsdays);
                    break;
                case "4":
                    ShowTipAndClearConsole();
                    WeekDayTraining(monthsDaysNotLeapYear, monthsDoomsdays, weekDays, random);
                    break;
            }
            MainMenu();
        }
        public static void ShowTipAndClearConsole()
        {
            Console.Clear();
            Console.WriteLine("Monday = 0, Tuesday = 1, Wednesday = 2, Thursday = 3, Friday = 4, Saturday = 5, Sunday = 6\n");
        }
        public static void CenturyDoomsday(Random random, string[] weekDays)
        {
            //Generate random year
            int randomYear = GetRandomYear(random);
            randomYear /= 100;
            randomYear *= 100;

            //Calculate century start day
            int[] centuryStartDays = new int[] { 6, 4, 2, 1 };
            int centuryStartDay = centuryStartDays[(randomYear / 100 - 13) % 4];

            Console.WriteLine($"What doomsday for {randomYear} century");

            //take guess from learner
            string guessStr = Console.ReadLine() ?? "-1";
            int guess = -1;
            try
            {
                guess = int.Parse(guessStr);
            }
            catch (Exception) { Console.WriteLine("Wrong"); }
            if(centuryStartDay == guess)
                Console.WriteLine($"Correct, it was {weekDays[centuryStartDay]}\n");
            else
                Console.WriteLine($"Wrong, it was {weekDays[centuryStartDay]}\n");
            if (guessStr == "return")
                MainMenu();
            else
                CenturyDoomsday(random, weekDays);
        }
        public static void YearDoomsday(Random random, Dictionary<string, int> monthDoomsdays, Dictionary<string, int> monthDays, string[] weekDays) 
        {
            //Generate random year
            int randomYear = GetRandomYear(random);

            //change days count and doomsdays if leap year
            if (randomYear % 4 == 0)
            {
                monthDays["February"] = 29;
                monthDoomsdays["January"] = 4;
                monthDoomsdays["February"] = 29;
            }
            else
            {
                monthDays["February"] = 28;
                monthDoomsdays["January"] = 3;
                monthDoomsdays["February"] = 28;
            }

            //Calculate century start day
            int[] centuryStartDays = new int[] { 6, 4, 2, 1 };
            int centuryStartDay = centuryStartDays[(randomYear / 100 - 13) % 4];

            //Calculate year start day
            int year = randomYear % 100 % 28;
            int doomsdayForThatYear = (centuryStartDay + (year + year / 4)) % 7;

            Console.WriteLine($"What doomsday for {randomYear} century");

            //take guess from learner
            string guessStr = Console.ReadLine() ?? "-1";
            int guess = -1;
            try
            {
                guess = int.Parse(guessStr);
            }
            catch (Exception) { Console.WriteLine("Wrong"); }
            if (doomsdayForThatYear == guess)
                Console.WriteLine($"Correct, it was {weekDays[doomsdayForThatYear]}\n");
            else
                Console.WriteLine($"Wrong, it was {weekDays[doomsdayForThatYear]}\n");
            if (guessStr == "return")
                MainMenu();
            else
                YearDoomsday(random, monthDoomsdays, monthDays, weekDays);
        }
        public static void MonthDoomsday(Random random, Dictionary<string, int> monthDoomsdays) 
        {
            int randomMonth = GetRandomMonth(random);
            Console.WriteLine($"What doomsday for {monthDoomsdays.ElementAt(randomMonth).Key}");

            //take guess from learner
            string guessStr = Console.ReadLine() ?? "-1";
            int guess = -1;
            try
            {
                guess = int.Parse(guessStr);
            }
            catch (Exception) { Console.WriteLine("Wrong"); }
            if (monthDoomsdays.ElementAt(randomMonth).Value == guess)
                Console.WriteLine($"Correct, it was {monthDoomsdays.ElementAt(randomMonth).Value} of {monthDoomsdays.ElementAt(randomMonth).Key}\n");
            else
                Console.WriteLine($"Wrong, it was {monthDoomsdays.ElementAt(randomMonth).Value} of {monthDoomsdays.ElementAt(randomMonth).Key}\n");
            if (guessStr == "return")
                MainMenu();
            else
                MonthDoomsday(random, monthDoomsdays);
        }
        public static void WeekDayTraining(Dictionary<string, int> monthDays, Dictionary<string, int> monthDoomsdays, string[] weekDays, Random random)
        {
            //Generate random year
            int randomYear = GetRandomYear(random);
            
            //change days count and doomsdays if leap year
            if (randomYear % 4 == 0)
            {
                monthDays["February"] = 29;
                monthDoomsdays["January"] = 4;
                monthDoomsdays["February"] = 29;
            }
            else 
            {
                monthDays["February"] = 28;
                monthDoomsdays["January"] = 3;
                monthDoomsdays["February"] = 28;
            }

            //Generate random month
            int randomMonth = GetRandomMonth(random);
            
            //Generate random day
            int randomDay = GetRandomDay(random, randomMonth, monthDays);
            
            //Calculate day
            int weekDay = GetRealWeekDay(monthDoomsdays, randomYear, randomMonth, randomDay);

            //take guess from learner
            Console.WriteLine($"What day of the week was {randomDay}th {monthDays.ElementAt(randomMonth).Key} of {randomYear}?\n" +
                $"Your guess is: ");
            
            string guessStr = Console.ReadLine() ?? "-1";
            int guess = -1;
            try
            {
                guess = int.Parse(guessStr);
            }
            catch (Exception) { Console.WriteLine("Wrong"); }

            //Check learner guess is right
            if(guess == weekDay)
                Console.WriteLine($"Correct, it was {weekDays[weekDay]}\n");
            else
                Console.WriteLine($"Wrong, it was {weekDays[weekDay]}\n");
            if (guessStr == "return")
                MainMenu();
            else
                WeekDayTraining(monthDays, monthDoomsdays, weekDays, random);
        }
        public static int GetRealWeekDay(Dictionary<string, int> monthDoomsdays, int randomYear, int randomMonth, int randomDay)
        {
            //Calculate century start day
            int[] centuryStartDays = new int[] { 6, 4, 2, 1 };
            int centuryStartDay = centuryStartDays[(randomYear / 100 - 13) % 4];

            //Calculate year start day
            int year = randomYear % 100 % 28;
            int doomsdayForThatYear = (centuryStartDay + (year + year / 4)) % 7;

            //Calculate day
            int doomsday = monthDoomsdays.ElementAt(randomMonth).Value;
            int weekDay = 0;
            if (randomDay >= doomsday)
            {
                weekDay = (randomDay - doomsday + doomsdayForThatYear) % 7;
            }
            else //randomDay < doomsday
            {
                for (int i = doomsday; i > randomDay; i--)
                {
                    if (doomsdayForThatYear < 0)
                        doomsdayForThatYear = 6;
                    doomsdayForThatYear--;
                }
                if (doomsdayForThatYear < 0)
                    doomsdayForThatYear = 6;
                weekDay = doomsdayForThatYear;
            }
            return weekDay;
        }
        public static int GetRandomYear(Random random)
        {
            int minYear = 1500;
            int maxYear = 2200;
            
            return random.Next(minYear, maxYear);
        }
        public static int GetRandomMonth(Random random)
        {
            return random.Next(0, 12);
        }
        public static int GetRandomDay(Random random, int randomMonth, Dictionary<string, int> monthDays)
        {
            return random.Next(1, monthDays.ElementAt(randomMonth).Value);
        }
    }
}