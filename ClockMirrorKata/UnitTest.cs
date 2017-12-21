using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Security.Policy;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ClockMirrorKata
{
    [TestClass]
    public class UnitTest
    {
        [TestMethod]
        public void Hour12Return11()
        {
            var expected = "11";
            var actual = ClockMirror.Hour(12, 1);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Hour3Return8()
        {
            var expected = "08";
            var actual = ClockMirror.Hour(3, 1);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Minute10Return50()
        {
            var expected = "50";
            var actual = ClockMirror.Minutes(10);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Minute0Return0()
        {
            var expected = "00";
            var actual = ClockMirror.Minutes(0);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Time1201te0Return1159()
        {
            var expected = "11:59";
            var actual = ClockMirror.FormatTime("12:01");
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Time1101te0Return1159()
        {
            var expected = "01:01";
            var actual = ClockMirror.FormatTime("10:59");
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Time311Return0949()
        {
            var expected = "08:49";
            var actual = ClockMirror.FormatTime("03:11");
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Time300Return0900()
        {
            var expected = "09:00";
            var actual = ClockMirror.FormatTime("03:00");
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Time12Return00()
        {
            var expected = "12:00";
            var actual = ClockMirror.FormatTime("12:00");
            Assert.AreEqual(expected, actual);
        }
    }

    public static class ClockMirror
    {
        public static string FormatTime(string time)
        {
            var h = Convert.ToInt32(time.Split(':')[0]);
            var min = Convert.ToInt32(time.Split(':')[1]);
            var hour = ClockMirror.Hour(h, min);
            var minutes = ClockMirror.Minutes(min);

            return $"{hour}:{minutes}";
        }

        public static string Hour(int h, int minute)
        {
            if (h == 12 && minute == 0)
            {
                return "12";
            }

            if (h < 3 && minute == 0)
            {
                return $"{12 - h}";
            }

            if (minute == 0)
            {
                return $"{0}{12 - h}";
            }

            if (h > 10) return $"{23 - h}";

            if (h > 1) return $"{0}{11 - h}";

            return $"{11 - h}";
        }

        public static string Minutes(int min)
        {
            if (min == 0) return "00";
            if (min > 50)
            {
                return $"{0}{(60 - min).ToString()}";
            }
            return (60 - min).ToString();
        }
    }
}
