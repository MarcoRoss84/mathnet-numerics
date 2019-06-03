using NUnit.Framework;
using System;
using System.Collections.Generic;
using MathNet.Numerics.DigitalFilters;
using System.Linq;

namespace MathNet.Numerics.Tests.DigitalFiltersTests
{
    [TestFixture, Category("DigitalFilters")]
    public class FilterApplicationTest
    {

        private float[] _x;
        private Filter _iirFilter, _firFilter;

        [SetUp]
        public void InitFilterData()
        {
            _x = new[] { -1.3499F, 3.0349F, 0.7254F, -0.0631F, 0.7147F, -0.2050F, -0.1241F, 1.4897F, 1.4090F, 1.4172F };
            _iirFilter = new Filter(
                new[] { 0.5690, 1.1381, 0.5690 },
                new[] { 1.0000, 0.9428, 0.3333 },
                true
            );
            _firFilter = new Filter(
                new[] { 0.5690, 1.1381, 0.5690 },
                true
            );
        }

        [Test]
        public void TestFilterNormalization()
        {
            Filter filter = new Filter(
                new[] { 2.5, 3.0 },
                new[] { 2, 3.0 },
                true
            );

            Assert.AreEqual(new[] { 1.25, 1.5 }, filter.B);
            Assert.AreEqual(new[] { 1, 1.5 }, filter.A);

        }

        [Test]
        public void TestIirFilter()
        {

            var expected = new[] { -0.7681F, 0.9149F, 2.4921F, -0.1378F, 0.0470F, 0.6625F, -0.5375F, 0.8757F, 1.7801F, 1.2875F };
            var actual = _iirFilter.FilterSignal(_x);
            Assert.That(actual, Is.EqualTo(expected).Within(0.00005F * 6));

        }

        [Test]
        public void TestCloneFilter()
        {

            var clone = _iirFilter.Clone();
            var result1 = _iirFilter.FilterSignal(_x);
            var result2 = clone.FilterSignal(_x);
            Assert.That(result1, Is.EqualTo(result2));

        }

        [Test]
        public void TestResetFilterState()
        {

            var actual1 = _iirFilter.FilterSignal(_x);
            var actual2 = _iirFilter.FilterSignal(_x);

            Assume.That(actual2, Is.Not.EqualTo(actual1).Within(0.00005F * 6));

            _iirFilter.ResetState();
            var actual3 = _iirFilter.FilterSignal(_x);

            Assert.That(actual3, Is.EqualTo(actual1).Within(0.00005F * 6));
        }

        [Test]
        public void TestResetFilterStateWithData()
        {
            Assert.Fail("Not implemented, yet");
            //using (var lp = butter(2, 10, 2, FilterType.LP))
            //{
            //    lp.ResetState(5F);
            //    Assert.That(lp.PastX, Is.All.EqualTo(5F));
            //    Assert.That(lp.PastY, Is.All.EqualTo(5F));
            //}
            //using (var hp = butter(2, 10, 2, FilterType.HP))
            //{
            //    hp.ResetState(5F);
            //    Assert.That(hp.PastX, Is.All.EqualTo(5F));
            //    Assert.That(hp.PastY, Is.All.EqualTo(0F));
            //}
        }

        [Test]
        public void TestIirFilterState()
        {

            var expected = new[] { -0.7681F, 0.9149F, 2.4921F, -0.1378F, 0.0470F, 0.6625F, -0.5375F, 0.8757F, 1.7801F, 1.2875F };

            var actual1 = _iirFilter.FilterSignal(_x.Take(5));
            var actual2 = _iirFilter.FilterSignal(_x.Skip(5));
            var actual = actual1.Concat(actual2);

            Assert.That(actual, Is.EqualTo(expected).Within(0.00005F * 6));

        }

        [Test]
        public void TestIirFilterStateVeryViewSamples()
        {

            var expected = new[] { -0.7681F, 0.9149F, 2.4921F, -0.1378F, 0.0470F, 0.6625F, -0.5375F, 0.8757F, 1.7801F, 1.2875F };

            var actual1 = _iirFilter.FilterSignal(_x.Take(1));
            var actual2 = _iirFilter.FilterSignal(_x.Skip(1).Take(1));
            var actual3 = _iirFilter.FilterSignal(_x.Skip(2).Take(1));
            var actual4 = _iirFilter.FilterSignal(_x.Skip(3));
            var actual = actual1.Concat(actual2).Concat(actual3).Concat(actual4);

            Assert.That(actual, Is.EqualTo(expected).Within(0.00005F * 6));

        }

        [Test]
        public void TestFirFilter()
        {
            var expected = new[] { -0.7681F, 0.1907F, 3.0986F, 2.5167F, 0.7477F, 0.6609F, 0.1028F, 0.5898F, 2.4265F, 3.2577F };
            var actual = _firFilter.FilterSignal(_x);
            Assert.That(actual, Is.EqualTo(expected).Within(0.00005F * 6));

        }

        [Test]
        public void TestFirFilterBigData()
        {

            var filter = new Filter(
                new[] { 0.5690, 1.1381, 0.5690, 1.1381, 0.5690 },
                true);

            for (int i = 0; i < 1000; i++)
            {
                var x = Generate.RandomSingle(1000, new Distributions.Normal());
                Assert.DoesNotThrow(() => filter.FilterSignal(x));
            }

        }

        [Test]
        public void TestFirFilterState()
        {

            var expected = new[] { -0.7681F, 0.1907F, 3.0986F, 2.5167F, 0.7477F, 0.6609F, 0.1028F, 0.5898F, 2.4265F, 3.2577F };

            var actual1 = _firFilter.FilterSignal(_x.Take(5));
            var actual2 = _firFilter.FilterSignal(_x.Skip(5));
            var actual = actual1.Concat(actual2);

            Assert.That(actual, Is.EqualTo(expected).Within(0.00005F * 6));

        }

        [Test]
        public void TestFirFilterStateSampleBySample()
        {

            var expected = _firFilter.FilterSignal(_x);
            _firFilter.ResetState();

            IEnumerable<float> actual = new float[0];
            for (int i = 0; i < _x.Length; i++)
            {
                actual = actual.Concat(_firFilter.FilterSignal(_x.Skip(i).Take(1)));
            }

            Assert.That(actual, Is.EqualTo(expected));

        }

        [Test]
        public void TestIirFilterStateSampleBySample()
        {
            var expected = _iirFilter.FilterSignal(_x);
            _iirFilter.ResetState();

            IEnumerable<float> actual = new float[0];
            for (int i = 0; i < _x.Length; i++)
            {
                actual = actual.Concat(_iirFilter.FilterSignal(_x.Skip(i).Take(1)));
            }

            Assert.That(actual, Is.EqualTo(expected));

        }

        [Test]
        public void FirFilterLotsOfData()
        {

            var x = Generate.RandomSingle(1000000, new Distributions.Normal());

            var filter = new Filter(new double[] { -1, 2, -1 }, false);
            var expected = filter.FilterSignal(x);

            IEnumerable<float> actual = new float[0];
            filter.ResetState();
            for (int i = 0; i < 1000000; i += 1000)
            {
                actual = actual.Concat(filter.FilterSignal(x.Skip(i).Take(1000)));
            }

            Assert.That(actual, Is.EqualTo(expected));

        }

        [Test]
        public void IirFilterLotsOfData()
        {
            Assert.Fail("Not implemented, yet");
            //using (ILScope.Enter())
            //{
            //    ILArray<float> x = tosingle(randn(1, 1000000));

            //    ILArray<float> expected = empty<float>();
            //    using (var filter = butter(1, 100, 10F, FilterType.HP))
            //    {
            //        expected.a = filter.FilterSignal(x);
            //    }

            //    ILArray<float> actual = empty<float>(0, 1);
            //    using (var filter = butter(1, 100, 10F, FilterType.HP))
            //    {
            //        for (int i = 0; i < 1000000; i += 1000)
            //        {
            //            using (ILScope.Enter())
            //            {
            //                actual.a = actual.Concat(filter.FilterSignal(x[r(i, i + 999)]), 0);
            //            }
            //        }
            //    }

            //    Assert.That(actual, Is.EqualTo(expected));

            //}
        }
    }
}
