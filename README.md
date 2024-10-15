# Brigadoon-Selenium-Data-Parser
Anonymized version of Selenium Data Scraper project.

# Web Scraping with Selenium and HtmlAgilityPack

This C# program automates web navigation and scraping using Selenium WebDriver with an Edge browser (can be changed to Chrome, Firefox, etc.). It leverages the **HtmlAgilityPack** to parse HTML data and extract specific information from a website. The data is then saved in a CSV format.

## Technologies Used
- **C#**
- **Selenium WebDriver** (for web automation)
- **HtmlAgilityPack** (for HTML parsing)
- **CSV output** (to save scraped data)

## Features
- Automates browser navigation to a specified URL.
- Searches for a specified term and grabs data from multiple pages.
- Extracts specific HTML elements using **XPath** and **CSS selectors**.
- Saves the extracted data into a CSV file for further processing.

## Setup Instructions

1. **Clone this repository:**
   ```bash
   git clone https://github.com/your-username/your-repo.git
   cd your-repo

2. Install Dependencies: You need to install the following NuGet packages:
   ```bash
   dotnet add package Selenium.WebDriver
   dotnet add package HtmlAgilityPack

3. Configure the WebDriver: The code is currently set up to use the EdgeDriver:
   ```csharp
      static IWebDriver driver = new EdgeDriver();

  If you want to use ChromeDriver or FirefoxDriver, you can modify this line accordingly.

4. Run the Program: After setting up the project and installing the necessary dependencies, you can run the program with:
   ```csharp
    dotnet run

## Usage
The program will navigate to the base URL and perform a search for the term provided in the Main() function. It scrapes data from the first 5 pages and saves it to a file named output.csv.

## Example
The code below demonstrates how the program navigates to StackOverflow Directory and searches for the term "Entity":
  ```csharp
     NavigateWeb("https://www.stackoverflow.com/directory", "Entity");
```

You can change the URL or the search term as needed for your scraping purposes.

## Output
The scraped data is saved into a CSV file (output.csv), with each row representing data extracted from the website.

## Error Handling
The program handles common web automation exceptions:
 - NoSuchElementException
 - ElementNotVisibleException
 - ElementNotSelectableException
   
In case an element is not found or cannot be interacted with, the program will catch the exception and log a message without crashing.

## License
This project is licensed under the MIT License. See the LICENSE file for more details.

## Contributions
Feel free to submit a pull request if you'd like to contribute or improve this project.
