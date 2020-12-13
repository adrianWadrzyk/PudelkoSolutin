using System;
using System.Collections;
using System.Globalization;

namespace PudelkoSolution
{
    public sealed class Pudelko : IEnumerable, IComparable<Pudelko>
    {
        public double a, b, c;

        public UnitOfMeasure unit;
        public double A
        {
            get
            {
                return (double)ToMeters(a, unit);
            }
        }
        public double B
        {
            get
            {
                return (double)ToMeters(b, unit);
            }
        }
        public double C
        {
            get
            {
                return (double)ToMeters(c, unit);
            }
        }
        public double Capacity => Math.Round(A * B * C, 9);
        public double Surface => Math.Round((2 * A * B) + (2 * A * C) + (2 * B * C), 6);

        private readonly double[] indexer;

        public object this[int index]
        {
            get
            {
                return indexer[index];
            }
        }
        public Pudelko(double? a = null, double? b = null, double? c = null, UnitOfMeasure unit = UnitOfMeasure.meter)
        {
            this.a = a.GetValueOrDefault(GetDefault(unit));
            this.b = b.GetValueOrDefault(GetDefault(unit));
            this.c = c.GetValueOrDefault(GetDefault(unit));
            this.unit = unit;
            indexer = new[] { A, B, C };
            CheckParam();
        }
        public double GetDefault(UnitOfMeasure unit)
        {
            switch (unit)
            {
                case UnitOfMeasure.meter:
                    return 0.1;
                case UnitOfMeasure.centimeter:
                    return 10;
                case UnitOfMeasure.milimeter:
                    return 100;
                default:
                    throw new ArgumentException();
            }
        }
        public void CheckParam()
        {
            if (A < 0.001 || B < 0.001 || C < 0.001)
            {
                throw new ArgumentOutOfRangeException();
            };
            if (A > 10 || B > 10 || C > 10)
            {
                throw new ArgumentOutOfRangeException();
            }
        }
        public decimal ToMeters(double sideLength, UnitOfMeasure metric)
        {
            switch (metric)
            {
                case UnitOfMeasure.centimeter:
                    return (decimal)Math.Floor(sideLength * 10) / 1000;
                case UnitOfMeasure.milimeter:
                    return (decimal)sideLength / 1000;
                default:
                    return (decimal)sideLength;
            }
        }
        public bool Equals(Pudelko other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other))
                return true;

            double[] sidesOther = { other.A, other.B, other.C };
            double[] sides = { A, B, C };
            Array.Sort(sidesOther);
            Array.Sort(sides);
            for (int i = 0; i <= 2; i++)
                if (sidesOther[i] != sides[i])
                    return false;
            return true;
        }

        public override bool Equals(object obj)
        {
            if (obj is Pudelko)
            {
                return Equals((Pudelko)obj);
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode() => (A, B, C).GetHashCode();

        public string ToString(string format, IFormatProvider provider = null)
        {
            if (String.IsNullOrEmpty(format)) format = "m";
            if (provider == null) provider = CultureInfo.CurrentCulture;

            switch (format)
            {
                case "m":
                    return $"{A.ToString("0.000", CultureInfo.InvariantCulture)} m × {B.ToString("0.000", CultureInfo.InvariantCulture)} m × {C.ToString("0.000", CultureInfo.InvariantCulture)} m";
                case "cm":
                    return $"{(A * 100).ToString("0.0", CultureInfo.InvariantCulture)} cm × {(B * 100).ToString("0.0", CultureInfo.InvariantCulture)} cm × {(C * 100).ToString("0.0", CultureInfo.InvariantCulture)} cm";
                case "mm":
                    return $"{(A * 1000).ToString("0", CultureInfo.InvariantCulture)} mm × {(B * 1000).ToString("0", CultureInfo.InvariantCulture)} mm × {(C * 1000).ToString("0", CultureInfo.InvariantCulture)} mm";
                default:
                    throw new FormatException("Error");
            }
        }

        public override string ToString()
        {
            return ToString("m", CultureInfo.CurrentCulture);
        }

        public string ToString(string format)
        {
            return ToString(format, CultureInfo.CurrentCulture);
        }

        public IEnumerator GetEnumerator()
        {
            return indexer.GetEnumerator();
        }

        public int CompareTo(Pudelko other)
        {
            if (other is null) return 1;
            if (this.Equals(other)) return 0;

            if (this.Capacity != other.Capacity)
                return this.Capacity.CompareTo(other.Capacity);

            if (!this.Surface.Equals(other.Surface))
                return this.Surface.CompareTo(other.Surface);

            return (this.A + this.B + this.C).CompareTo(other.A + other.B + other.C);
        }

        public static bool operator ==(Pudelko p1, Pudelko p2) => Equals(p1, p2);
        public static bool operator !=(Pudelko p1, Pudelko p2) => !(p1 == p2);

        public static explicit operator double[](Pudelko p1)
        {
            double[] temp = { p1.A, p1.B, p1.C };
            return temp;
        }

        public static implicit operator Pudelko(ValueTuple<int, int, int> dimension)
        {

            return new Pudelko(dimension.Item1, dimension.Item2, dimension.Item3, UnitOfMeasure.milimeter);
        }

        public static Pudelko Parse(string parse)
        {
            string[] tmpStr = parse.Split(' ');
            double a = double.Parse(tmpStr[0], CultureInfo.InvariantCulture);
            double b = double.Parse(tmpStr[3], CultureInfo.InvariantCulture);
            double c = double.Parse(tmpStr[6], CultureInfo.InvariantCulture);

            if (tmpStr[1] != tmpStr[4] || tmpStr[1] != tmpStr[7])
                throw new FormatException();

            switch (tmpStr[1])
            {
                case "m":
                    return new Pudelko(a, b, c, UnitOfMeasure.meter);
                case "cm":
                    return new Pudelko(a, b, c, UnitOfMeasure.centimeter);
                case "mm":
                    return new Pudelko(a, b, c, UnitOfMeasure.milimeter);
                default:
                    throw new FormatException();
            }
        }
    }
    public enum UnitOfMeasure
    {
        milimeter,
        centimeter,
        meter
    }
}
