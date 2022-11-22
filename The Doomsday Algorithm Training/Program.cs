namespace The_Doomsday_Algorithm_Training
{
    internal class Program
    {
        static void Main(string[] args)
        {
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
            Console.WriteLine("Hello to Doomsday Algorithm Training!\nLet's do some training\n" +
                "Monday = 0, Tuesday = 1, Wednesday = 2, Thursday = 3, Friday = 4, Saturday = 5, Sunday = 6\n");
            TrainEngine(monthsDaysNotLeapYear, monthsDoomsdays);
        }
        public static void TrainEngine(Dictionary<string, int> monthDays, Dictionary<string, int> monthDoomsdays)
        {
            string[] weekDays = new string[] { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };
            Random random = new Random();

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
            TrainEngine(monthDays, monthDoomsdays);
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