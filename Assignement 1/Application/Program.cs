using Algorithms.Utilities;
using static Algorithms.Utilities.AlgorithmsUtilities;

try
{
    do 
    {
        int[] originalArray = [] ;
        Dictionary<string, SortingDelegate> sortingAlgorithms =[];
        // Get the array size from the user
        Console.WriteLine("Welcome to the Sorting Application!");   

        // Ask the user whether they want to run all algorithms or a single one
        Console.WriteLine("\nDo you want to run:");
        Console.WriteLine("1. A specific sorting algorithm");
        Console.WriteLine("2. All sorting algorithms");
        Console.WriteLine("3. Linear, Binary and lambda Search for n=100,000");
        Console.WriteLine("4. Employee details using c# methods");
        Console.Write("Enter your choice: ");

        if (!int.TryParse(Console.ReadLine(), out int choice) || (choice != 1 && choice != 2 && choice != 3 && choice !=4))
        {
            Console.WriteLine("Invalid choice. Exiting...");
            return;
        }
        if (choice <3)
        {
            Console.Write("Enter the size of the array (greater than 0): ");
            if (!int.TryParse(Console.ReadLine(), out int arraySize) || arraySize <= 0)
            {
                Console.WriteLine("Invalid array size. Exiting...");
                return;
            }
            // Prepare the original array
            originalArray = Prepare(arraySize);
            if (originalArray.Length <= 100000)
            {
                Console.WriteLine("\nOriginal Array: " + string.Join(", ", originalArray));
            }

            // Dictionary to map algorithms
            sortingAlgorithms = new Dictionary<string, SortingDelegate>
            {
                { "InsertionSort", InsertionSort },
                { "SelectionSort", SelectionSort },
                { "BubbleSort", BubbleSort },
                { "MergeSort", MergeSort },
                { "QuickSort", QuickSort },
                { "SortByLambda", SortByLambda }
            };
        }

        // If user chooses to run a single algorithm
        if (choice == 1)
        {
            Console.WriteLine("\nSelect a sorting algorithm:");
            int i = 1;
            foreach (var algorithm in sortingAlgorithms.Keys)
            {
                Console.WriteLine($"{i}. {algorithm}");
                i++;
            }

            Console.Write("Enter your choice (1-6): ");
            if (!int.TryParse(Console.ReadLine(), out int algorithmChoice) || algorithmChoice < 1 || algorithmChoice > 6)
            {
                Console.WriteLine("Invalid choice. Exiting...");
                return;
            }

            // Run the selected algorithm
            string selectedAlgorithm = sortingAlgorithms.Keys.ToArray()[algorithmChoice - 1];
            Console.WriteLine($"\nRunning {selectedAlgorithm}...");
            await DisplayRunningTime(originalArray, sortingAlgorithms[selectedAlgorithm]);
        }
        // If user chooses to run all algorithms
        else if (choice == 2)
        {
            Console.WriteLine("\nRunning all sorting algorithms...");
            var results = new Dictionary<string, double>();

            foreach (var algorithm in sortingAlgorithms)
            {
                await DisplayRunningTime((int[])originalArray.Clone(), algorithm.Value);
            }

            // Display results for all algorithms
            foreach (var result in results)
            {
                Console.WriteLine($"{result.Key}: {result.Value} ms");
            }
        }
        else if (choice == 3)
        {
            // Prepare the original array
            int[] array = Prepare(100000);
            int[] searchArray = (int[])array.Clone(); // Clone the array for search

            // Prompt user to select a case to test
            Console.WriteLine("\nChoose a case to test:");
            Console.WriteLine("1. Best Case");
            Console.WriteLine("2. Average Case");
            Console.WriteLine("3. Worst Case");
            Console.Write("Enter your choice (1/2/3): ");

            // Parse user input and validate choice
            if (int.TryParse(Console.ReadLine(), out int caseChoice))
            {
                Console.WriteLine(searchArray.Length);
                // Determine target based on user input
                int target;
                switch (caseChoice)
                {
                    case 1:
                        target = searchArray[0];  // Best Case: First element
                        Console.WriteLine($"\nBest Case Target (First Index): {target}");
                        break;
                    case 2:
                        target = searchArray[searchArray.Length / 2];  // Average Case: Middle element
                        Console.WriteLine($"Average Case Target (Middle Index): {target}");
                        break;
                    case 3:
                        target = searchArray[searchArray.Length-1];  // Worst Case: Last element
                        Console.WriteLine($"Worst Case Target (Last Index): {target}");
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please choose 1, 2, or 3.");
                        return;
                }

                // Execute search and display timings for the all case

                // Linear Search: No sorting required
                Console.WriteLine("\nRunning LinearSearch...");
               await DisplayRunningTime(searchArray, LinearSearch, target);

                // Binary Search: Ensure array is sorted before using binary search
                Console.WriteLine("\nRunning BinarySearch...");
                int[] sortedArray = (int[])array.Clone();  // Make sure to use the original array for sorting
                AlgorithmsUtilities.QuickSort(sortedArray);  // Sorting the array (use qucikSort for simplicity)
               await DisplayRunningTime(sortedArray, BinarySearch, target);  

                // SearchByLambda: Running SearchByLambda on the original search array
                Console.WriteLine("\nRunning SearchByLambda...");
               await DisplayRunningTime(searchArray, SearchByLambda, target);
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a number (1/2/3).");
            }
        }
        else if(choice ==4 )
        {
            // Get the current solution directory path
            string solutionPath = AppDomain.CurrentDomain.BaseDirectory;
            // Since the above method returns a bin path i clipped it to make sure that it reads from my defined path 
            string? trimmedPath = Directory.GetParent(solutionPath)?.Parent?.Parent?.Parent?.Parent?.FullName;

            // Combine with the file name to get the full path
            string filePath = Path.Combine(trimmedPath, "Employee.txt");

            // Ensure the file exists
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"File not found: {filePath}");
                return;  
            }
            // Load employees from the file
            var employees = File.ReadAllLines(filePath)
                .Select(line => Employee.Parse(line))
                .ToList();

            // Filter: Get employees' names containing "an"
            var filteredNames = employees
                .Where(emp => emp.Name.Contains("an"))
                .Select(emp => emp.Name)
                .ToList();

            Console.WriteLine("Filtered Names:");
            foreach (var name in filteredNames)
            {
                Console.WriteLine(name);
            }

            // Map: Get a list of just the names
            var namesList = employees.Select(emp => emp.Name).ToList();

            Console.WriteLine("\nList of Names:");
            foreach (var name in namesList)
            {
                Console.WriteLine(name);
            }

            // Reduce: Calculate the total years of experience
            var totalExperience = employees.Aggregate(0, (sum, emp) => sum + emp.YearsOfExperience);

            Console.WriteLine($"\nTotal Years of Experience: {totalExperience}");
        }
        // Ask the user if they want to continue
        Console.Write("\nDo you want to perform another operation? (yes/no): ");
        string continueChoice = Console.ReadLine()?.Trim().ToLower();
        if (continueChoice != "yes" && continueChoice != "y")
        {
            Console.WriteLine("Exiting the application. Goodbye!");
            break;
        }
    }
    while(true);
}
catch (Exception e)
{
    Console.WriteLine(e.Message);
}

