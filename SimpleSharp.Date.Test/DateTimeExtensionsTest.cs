using System;

using NUnit.Framework;

namespace SimpleSharp.Date.Test
{
    [TestFixture]
    public class DateTimeExtensionsTest
    {
        [Test]
        public void TestStartDateOfWeek_WithFirstWeek_ExpectStartDateToBeLastWeekOfPreviousYear()
        {
            //Arrange
            int year = 2015;
            int week = 1;           
            DateTime expectedFirstWeek = new DateTime(2014, 12, 28);

            //Act
            var startOfFirstWeek = DateTimeExtensions.StartDateOfWeek(year, week);

            //Assert
            Assert.AreEqual(expectedFirstWeek, startOfFirstWeek);
        }

        [Test]
        public void TestStartDateOfWeek_WithWeekInMiddleOfTheYear_ExpectCorrectStartDate()
        {
            //Arrange
            int year = 2014;
            int week = 30;
            DateTime expectedStartOfWeek = new DateTime(2014, 7, 20);

            //Act
            var startOfWeek = DateTimeExtensions.StartDateOfWeek(year, week);

            //Assert
            Assert.AreEqual(expectedStartOfWeek, startOfWeek);
        }

        [Test]
        public void TestStartDateOfWeek_WithLastWeekOfTheYear_ExpectCorrectStartDate()
        {
            //Arrange
            int year = 2014;
            int week = 52;
            DateTime expectedStartOfWeek = new DateTime(2014, 12, 21);

            //Act
            var startOfWeek = DateTimeExtensions.StartDateOfWeek(year, week);

            //Assert
            Assert.AreEqual(expectedStartOfWeek, startOfWeek);
        }

        [Test]
        public void TestEndDateOfWeek_WithFirstWeek_ExpectCorrectEndDate()
        {
            //Arrange
            int year = 2015;
            int week = 1;
            DateTime expectedLastWeek = new DateTime(2015, 1, 3);

            //Act
            var endOfFirstWeek = DateTimeExtensions.EndDateOfWeek(year, week);

            //Assert
            Assert.AreEqual(expectedLastWeek, endOfFirstWeek);
        }

        [Test]
        public void TestEndDateOfWeek_WithWeekInMiddleOfTheYear_ExpectCorrectEndDate()
        {
            //Arrange
            int year = 2014;
            int week = 30;
            DateTime expectedEndOfWeek = new DateTime(2014, 7, 26);

            //Act
            var endOfWeek = DateTimeExtensions.EndDateOfWeek(year, week);

            //Assert
            Assert.AreEqual(expectedEndOfWeek, endOfWeek);
        }

        [Test]
        public void TestEndDateOfWeek_WithLastWeekOfTheYear_ExpectCorrectEndDate()
        {
            //Arrange
            int year = 2014;
            int week = 52;
            DateTime expectedEndOfWeek = new DateTime(2014, 12, 27);

            //Act
            var endOfWeek = DateTimeExtensions.EndDateOfWeek(year, week);

            //Assert
            Assert.AreEqual(expectedEndOfWeek, endOfWeek);
        }

        [Test]
        public void TestYesterdayUTC_ExpectYesterdayDateInUTC()
        {
            //Arrange
            TimeSpan defaultOffsetFromUTC = TimeSpan.FromHours(-4);
            DateTime defaultDateTime = new DateTime(2014, 10,23, 4, 55, 34);
            DateRangeValue expectedDateRange = new DateRangeValue(new DateTime(2014, 10, 22, 4, 0, 0), new DateTime(2014, 10, 23, 3, 59, 59, 999));
            
            //Act
            var yesterdayUTC = DateTimeExtensions.YesterdayUTC(defaultOffsetFromUTC, defaultDateTime);

            //Assert
            Assert.AreEqual(expectedDateRange, yesterdayUTC);
        }

        [Test]
        public void TestPastWeekUTC_ExpectCorrectDates()
        {
            //Arrange
            TimeSpan defaultOffsetFromUTC = TimeSpan.FromHours(-4);
            DateTime defaultDateTime = new DateTime(2014, 10, 23, 4, 55, 34);
            DateRangeValue expectedDateRange = new DateRangeValue(new DateTime(2014, 10, 16, 4, 0, 0), new DateTime(2014, 10, 23, 3, 59, 59, 999));

            //Act
            var pastWeekUTC = DateTimeExtensions.PastWeekUTC(defaultOffsetFromUTC, defaultDateTime);

            //Assert
            Assert.AreEqual(expectedDateRange, pastWeekUTC);
        }

        [Test]
        public void TestLastWeekUTC_ExpectCorrectDates()
        {
            //Arrange
            TimeSpan defaultOffsetFromUTC = TimeSpan.FromHours(-4);
            DateTime defaultDateTime = new DateTime(2014, 10, 23, 4, 55, 34);
            DateRangeValue expectedDateRange = new DateRangeValue(new DateTime(2014, 10, 12, 4, 0, 0), new DateTime(2014, 10, 19, 3, 59, 59, 999));

            //Act
            var lastWeekUTC = DateTimeExtensions.LastWeekUTC(defaultOffsetFromUTC, defaultDateTime);

            //Assert
            Assert.AreEqual(expectedDateRange, lastWeekUTC);
        }

        [Test]
        public void TestLastTwoWeekUTC_ExpectCorrectDates()
        {
            //Arrange
            TimeSpan defaultOffsetFromUTC = TimeSpan.FromHours(-4);
            DateTime defaultDateTime = new DateTime(2014, 10, 23, 4, 55, 34);
            DateRangeValue expectedDateRange = new DateRangeValue(new DateTime(2014, 10, 5, 4, 0, 0), new DateTime(2014, 10, 19, 3, 59, 59, 999));

            //Act
            var lastTwoWeeksUTC = DateTimeExtensions.LastTwoWeeksUTC(defaultOffsetFromUTC, defaultDateTime);

            //Assert
            Assert.AreEqual(expectedDateRange, lastTwoWeeksUTC);
        }

        [Test]
        public void TestLastFourWeeksUTC_ExpectCorrectDates()
        {
            //Arrange
            TimeSpan defaultOffsetFromUTC = TimeSpan.FromHours(-4);
            DateTime defaultDateTime = new DateTime(2014, 10, 23, 4, 55, 34);
            DateRangeValue expectedDateRange = new DateRangeValue(new DateTime(2014, 9, 21, 4, 0, 0), new DateTime(2014, 10, 19, 3, 59, 59, 999));

            //Act
            var lastFourWeeksUTC = DateTimeExtensions.LastFourWeeksUTC(defaultOffsetFromUTC, defaultDateTime);

            //Assert
            Assert.AreEqual(expectedDateRange, lastFourWeeksUTC);
        }

        [Test]
        public void TestMonthToDateUTC_ExpectCorrectDates()
        {
            //Arrange
            TimeSpan defaultOffsetFromUTC = TimeSpan.FromHours(-4);
            DateTime defaultDateTime = new DateTime(2014, 10, 23, 4, 55, 34);
            DateRangeValue expectedDateRange = new DateRangeValue(new DateTime(2014, 10, 1, 4, 0, 0), defaultDateTime);

            //Act
            var monthToDateUTC = DateTimeExtensions.MonthToDateUTC(defaultOffsetFromUTC, defaultDateTime);

            //Assert
            Assert.AreEqual(expectedDateRange, monthToDateUTC);
        }

        [Test]
        public void TestLastMonthUTC_ExpectCorrectDates()
        {
            //Arrange
            TimeSpan defaultOffsetFromUTC = TimeSpan.FromHours(-4);
            DateTime defaultDateTime = new DateTime(2014, 10, 23, 4, 55, 34);
            DateRangeValue expectedDateRange = new DateRangeValue(new DateTime(2014, 9, 1, 4, 0, 0), new DateTime(2014, 10, 1, 3, 59, 59, 999));

            //Act
            var lastMonthUTC = DateTimeExtensions.LastMonthUTC(defaultOffsetFromUTC, defaultDateTime);

            //Assert
            Assert.AreEqual(expectedDateRange, lastMonthUTC);
        }

        [Test]
        public void TestLastYearUTC_ExpectCorrectDates()
        {
            //Arrange
            TimeSpan defaultOffsetFromUTC = TimeSpan.FromHours(-4);
            DateTime defaultDateTime = new DateTime(2014, 10, 23, 4, 55, 34);
            DateRangeValue expectedDateRange = new DateRangeValue(new DateTime(2013, 1, 1, 4, 0, 0), new DateTime(2014, 1, 1, 3, 59, 59, 999));

            //Act
            var lastYearUTC = DateTimeExtensions.LastYearUTC(defaultOffsetFromUTC, defaultDateTime);

            //Assert
            Assert.AreEqual(expectedDateRange, lastYearUTC);
        }

        [Test]
        public void TestYearToDateUTC_ExpectCorrectDates()
        {
            //Arrange
            TimeSpan defaultOffsetFromUTC = TimeSpan.FromHours(-4);
            DateTime defaultDateTime = new DateTime(2014, 10, 23, 4, 55, 34);
            DateRangeValue expectedDateRange = new DateRangeValue(new DateTime(2014, 1, 1, 4, 0, 0), defaultDateTime);

            //Act
            var yearToDateUTC = DateTimeExtensions.YearToDateUTC(defaultOffsetFromUTC, defaultDateTime);

            //Assert
            Assert.AreEqual(expectedDateRange, yearToDateUTC);
        }        
    }
}
