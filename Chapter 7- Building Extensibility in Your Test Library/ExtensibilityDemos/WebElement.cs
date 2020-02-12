﻿using System;
using System.Collections.Generic;
using System.Threading;
using ExtensibilityDemos.Locators;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace ExtensibilityDemos
{
    public class WebElement : Element
    {
        private readonly IWebDriver _webDriver;
        private readonly IWebElement _webElement;
        private readonly ElementFinderService _elementFinderService;
        private readonly By _by;

        public WebElement(IWebDriver webDriver, IWebElement webElement, By by)
        {
            _webDriver = webDriver;
            _webElement = webElement;
            _by = by;
            _elementFinderService = new ElementFinderService(webElement, _webDriver);
        }

        public override By By => _by;

        public override string Text => _webElement?.Text;

        public override bool? Enabled => _webElement?.Enabled;

        public override bool? Displayed => _webElement?.Displayed;

        public override void Click()
        {
            WaitToBeClickable(By);
            _webElement?.Click();
        }

        public override List<Element> FindAllByClass(string cssClass)
        {
            var findStrategy = new ClassFindStrategy(cssClass);
            var nativeElements = _elementFinderService.FindAll(findStrategy);
            var resultElements = new List<Element>();
            foreach (var nativeElement in nativeElements)
            {
                resultElements.Add(new WebElement(_webDriver, nativeElement, findStrategy.Convert()));
            }
            return resultElements;
        }

        public override List<Element> FindAllById(string id)
        {
            var findStrategy = new IdFindStrategy(id);
            var nativeElements = _elementFinderService.FindAll(findStrategy);
            var resultElements = new List<Element>();
            foreach (var nativeElement in nativeElements)
            {
                resultElements.Add(new WebElement(_webDriver, nativeElement, findStrategy.Convert()));
            }
            return resultElements;
        }

        public override List<Element> FindAllByTag(string tag)
        {
            var findStrategy = new TagFindStrategy(tag);
            var nativeElements = _elementFinderService.FindAll(findStrategy);
            var resultElements = new List<Element>();
            foreach (var nativeElement in nativeElements)
            {
                resultElements.Add(new WebElement(_webDriver, nativeElement, findStrategy.Convert()));
            }
            return resultElements;
        }

        public override List<Element> FindAllByCss(string css)
        {
            var findStrategy = new CssFindStrategy(css);
            var nativeElements = _elementFinderService.FindAll(findStrategy);
            var resultElements = new List<Element>();
            foreach (var nativeElement in nativeElements)
            {
                resultElements.Add(new WebElement(_webDriver, nativeElement, findStrategy.Convert()));
            }
            return resultElements;
        }

        public override List<Element> FindAllByLinkText(string linkText)
        {
            var findStrategy = new LinkTextFindStrategy(linkText);
            var nativeElements = _elementFinderService.FindAll(findStrategy);
            var resultElements = new List<Element>();
            foreach (var nativeElement in nativeElements)
            {
                resultElements.Add(new WebElement(_webDriver, nativeElement, findStrategy.Convert()));
            }
            return resultElements;
        }

        public override List<Element> FindAllByXPath(string xpath)
        {
            var findStrategy = new XPathFindStrategy(xpath);
            var nativeElements = _elementFinderService.FindAll(findStrategy);
            var resultElements = new List<Element>();
            foreach (var nativeElement in nativeElements)
            {
                resultElements.Add(new WebElement(_webDriver, nativeElement, findStrategy.Convert()));
            }
            return resultElements;
        }

        public override Element FindByClass(string cssClass)
        {
            var findStrategy = new ClassFindStrategy(cssClass);
            var nativeElement = _elementFinderService.Find(findStrategy);
            return new WebElement(_webDriver, nativeElement, findStrategy.Convert());
        }

        public override Element FindById(string id)
        {
            var findStrategy = new IdFindStrategy(id);
            var nativeElement = _elementFinderService.Find(findStrategy);
            return new WebElement(_webDriver, nativeElement, findStrategy.Convert());
        }

        public override Element FindByTag(string tag)
        {
            var findStrategy = new TagFindStrategy(tag);
            var nativeElement = _elementFinderService.Find(findStrategy);
            return new WebElement(_webDriver, nativeElement, findStrategy.Convert());
        }

        public override Element FindByXPath(string xpath)
        {
            var findStrategy = new XPathFindStrategy(xpath);
            var nativeElement = _elementFinderService.Find(findStrategy);
            return new WebElement(_webDriver, nativeElement, findStrategy.Convert());
        }

        public override Element FindByCss(string css)
        {
            var findStrategy = new CssFindStrategy(css);
            var nativeElement = _elementFinderService.Find(findStrategy);
            return new WebElement(_webDriver, nativeElement, findStrategy.Convert());
        }

        public override Element FindByLinkText(string linkText)
        {
            var findStrategy = new LinkTextFindStrategy(linkText);
            var nativeElement = _elementFinderService.Find(findStrategy);
            return new WebElement(_webDriver, nativeElement, findStrategy.Convert());
        }

        ////public override Element FindElement(By locator)
        ////{
        ////    return new WebElement(_webDriver, _webElement?.FindElement(locator), locator);
        ////}

        public override string GetAttribute(string attributeName)
        {
            return _webElement?.GetAttribute(attributeName);
        }

        public override void TypeText(string text)
        {
            Thread.Sleep(500);
            _webElement?.Clear();
            _webElement?.SendKeys(text);
        }

        public override void WaitToExists()
        {
            var webDriverWait = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(30));
            webDriverWait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By));
        }

        private void WaitToBeClickable(By by)
        {
            var webDriverWait = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(30));
            webDriverWait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(by));
        }

        public override List<Element> FindAll(FindStrategy findStrategy)
        {
            var nativeElements = _elementFinderService.FindAll(findStrategy);
            var resultElements = new List<Element>();
            foreach (var nativeElement in nativeElements)
            {
                resultElements.Add(new WebElement(_webDriver, nativeElement, findStrategy.Convert()));
            }
            return resultElements;
        }

        public override Element Find(FindStrategy findStrategy)
        {
            var nativeElement = _elementFinderService.Find(findStrategy);
            return new WebElement(_webDriver, nativeElement, findStrategy.Convert());
        }
    }
}
