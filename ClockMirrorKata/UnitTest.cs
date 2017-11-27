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
            var actual = ClockMirror.hour(12);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Hour3Return8()
        {
            var expected = "08";
            var actual = ClockMirror.hour(3);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Minute10Return50()
        {
            var expected = "50";
            var actual = ClockMirror.minutes(10);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Minute0Return0()
        {
            var expected = "00";
            var actual = ClockMirror.minutes(0);
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
        public void Time311Return0949()
        {
            var expected = "08:49";
            var actual = ClockMirror.FormatTime("03:11");
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
            string hour = ClockMirror.hour(h, min);
            string minutes = ClockMirror.minutes(min);

            return $"{hour}:{minutes}";
        }

        public static string hour(int h, int minute = 01)
        {
            if (h == 12 && minute == 00)
            {
                return "12";
            }
            if (h == 12)
                return "11";
            if (h == 11)
                return "12";
            return $"{0}{11 - h}";
        }

        public static string minutes(int min)
        {
            if (min == 0)
                return "00";
            return (60-min).ToString();
        }
}
    }
