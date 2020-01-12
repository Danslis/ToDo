using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using System.IO;
using System.Reflection;
using OpenQA.Selenium;
using System.Threading;

namespace FrontToDoIntegrationTests.Test.HomePageTests
{
    [TestFixture]
    public class HomePageTests
    {
        private string url = "https://localhost:44326/";
        private void AddCard(ChromeDriver driver)
        {
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl(url);
            Delay(2);
            driver.FindElement(By.Id("add-button")).Click();
            Delay(2);
            driver.FindElement(By.Id("input-name")).Clear();
            driver.FindElement(By.Id("input-name")).SendKeys("Тест добавить карточку Selenium");
            Delay(2);
            driver.FindElement(By.XPath("(.//*[normalize-space(text()) and normalize-space(.)='Название карточки'])[1]/following::span[2]")).Click();
            Delay(2);
            driver.FindElement(By.XPath("(.//*[normalize-space(text()) and normalize-space(.)='Применить'])[1]/following::span[1]")).Click();
            Delay(2);
            driver.FindElement(By.XPath("(.//*[normalize-space(text()) and normalize-space(.)='Время исполнения'])[1]/following::span[2]")).Click();
            Delay(2);
            driver.FindElement(By.XPath("(.//*[normalize-space(text()) and normalize-space(.)='Применить'])[1]/following::span[1]")).Click();
            Delay(2);
            driver.FindElement(By.XPath("(.//*[normalize-space(text()) and normalize-space(.)='Отмена'])[1]/following::span[1]")).Click();
            Delay(2);
        }

        private void EditCard(ChromeDriver driver)
        {
            driver.FindElement(By.Name("Тест добавить карточку Selenium")).FindElement(By.ClassName("edit-link")).Click();
            Delay(2);
            driver.FindElement(By.Id("mat-input-1")).Clear();
            Delay(2);
            driver.FindElement(By.Id("mat-input-1")).SendKeys("Тест добавить карточку Selenium отредактированнанная");
            Delay(2);
            driver.FindElement(By.XPath("(.//*[normalize-space(text()) and normalize-space(.)='Отмена'])[1]/following::button[1]")).Click();
            Delay(2);
            driver.FindElement(By.Name("Тест добавить карточку Selenium отредактированнанная")).FindElement(By.ClassName("delete-link")).Click();
            Delay(2);
            driver.FindElement(By.XPath("(.//*[normalize-space(text()) and normalize-space(.)='Отменить'])[1]/following::span[1]")).Click();
            Delay(2);
        }
        private void DeleteCard(ChromeDriver driver)
        {
            Delay(2);
            driver.FindElement(By.Name("Тест добавить карточку Selenium")).FindElement(By.ClassName("delete-link")).Click();
            Delay(2);
            driver.FindElement(By.XPath("(.//*[normalize-space(text()) and normalize-space(.)='Отменить'])[1]/following::span[1]")).Click();
            Delay(2);
        }
        private void Done(ChromeDriver driver)
        {
            driver.FindElement(By.Name("Тест добавить карточку Selenium")).FindElement(By.ClassName("done-link")).Click();
            Delay(2);
            driver.FindElement(By.Name("Тест добавить карточку Selenium")).FindElement(By.ClassName("delete-link")).Click();
            Delay(2);
            driver.FindElement(By.XPath("(.//*[normalize-space(text()) and normalize-space(.)='Отменить'])[1]/following::span[1]")).Click();
            Delay(2);
        }

        public void Delay(int seconds = 10)
        {
            Thread.Sleep(seconds* 1000);
        }

        [Test]
        public void AddCard()
        {
            using (var driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)))
            {
               AddCard(driver);
               DeleteCard(driver);
               driver.Quit();
            }
        }
        [Test]
        public void DeleteCard()
        {
            using (var driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)))
            {
               AddCard(driver);
               DeleteCard(driver);
               driver.Quit();
            }
        }
        [Test]
        public void EditCard()
        {
            using (var driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)))
            {
                AddCard(driver);
                EditCard(driver);
                driver.Quit();
            }
        }
        [Test]
        public void Done()
        {
            using (var driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)))
            {
                AddCard(driver);
                Done(driver);
                driver.Quit();
            }
        }
    }
}
