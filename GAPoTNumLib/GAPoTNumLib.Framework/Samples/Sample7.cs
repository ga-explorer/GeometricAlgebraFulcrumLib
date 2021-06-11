using System;
using System.Globalization;
using GAPoTNumLib.GAPoT;

namespace GAPoTNumLib.Framework.Samples
{
    public static class Sample7
    {
        public static void Execute()
        {
            CultureInfo.CurrentCulture = new CultureInfo("en-US");

            //Define basis vectors before rotation
            var u1 = "1<1>".GaPoTNumParseVector();
            var u2 = "1<2>".GaPoTNumParseVector();
            var u3 = "1<3>".GaPoTNumParseVector();
            var u4 = "1<4>".GaPoTNumParseVector();
            var u1234 = GaPoTNumUtils.OuterProduct(u1, u2, u3, u4);
            
            Console.WriteLine($@"u1 = {u1.TermsToText()}");
            Console.WriteLine($@"u2 = {u2.TermsToText()}");
            Console.WriteLine($@"u3 = {u3.TermsToText()}");
            Console.WriteLine($@"u4 = {u4.TermsToText()}");
            Console.WriteLine();
            
            Console.WriteLine($@"u1234 = {u1234.TermsToText()}");
            Console.WriteLine($@"inverse(u1234) = {u1234.Inverse().TermsToText()}");
            Console.WriteLine();
            
            //Define basis vectors after rotation
            var c1 = "1<1>, -1<4>".GaPoTNumParseVector() / Math.Sqrt(2);
            var c2 = "-1<1>, 2<2>, -1<4>".GaPoTNumParseVector() / Math.Sqrt(6);
            var c3 = "-1<1>, -1<2>, 3<3>, -1<4>".GaPoTNumParseVector() / Math.Sqrt(12);
            var c4 = -GaPoTNumUtils.OuterProduct(c1, c2, c3).Gp(u1234.Inverse()).GetVectorPart();
            var c1234 = GaPoTNumUtils.OuterProduct(c1, c2, c3, c4);
            
            Console.WriteLine($@"c1 = {c1.TermsToText()}");
            Console.WriteLine($@"c2 = {c2.TermsToText()}");
            Console.WriteLine($@"c3 = {c3.TermsToText()}");
            Console.WriteLine($@"c4 = {c4.TermsToText()}");
            Console.WriteLine();

            //Make sure c1,c2,c3,c4 are mutually orthonormal
            Console.WriteLine($@"c1 . c1 = {c1.DotProduct(c1):G}");
            Console.WriteLine($@"c2 . c2 = {c2.DotProduct(c2):G}");
            Console.WriteLine($@"c3 . c3 = {c3.DotProduct(c3):G}");
            Console.WriteLine($@"c4 . c4 = {c4.DotProduct(c4):G}");
            Console.WriteLine($@"c1 . c2 = {c1.DotProduct(c2):G}");
            Console.WriteLine($@"c2 . c3 = {c2.DotProduct(c3):G}");
            Console.WriteLine($@"c3 . c4 = {c3.DotProduct(c4):G}");
            Console.WriteLine($@"c4 . c1 = {c4.DotProduct(c1):G}");
            Console.WriteLine($@"c1 . c3 = {c1.DotProduct(c3):G}");
            Console.WriteLine($@"c2 . c4 = {c2.DotProduct(c4):G}");
            Console.WriteLine();
            
            Console.WriteLine($@"c1234 = {c1234.TermsToText()}");
            Console.WriteLine($@"inverse(c1234) = {c1234.Inverse().TermsToText()}");
            Console.WriteLine();

            //Find angles between basis vectors
            var ang1 = u1.GetAngle(c1);
            var ang2 = u2.GetAngle(c2);
            var ang3 = u3.GetAngle(c3);
            var ang4 = u4.GetAngle(c4);

            Console.WriteLine($@"Angle1 = {ang1.RadiansToDegrees():G}");
            Console.WriteLine($@"Angle2 = {ang2.RadiansToDegrees():G}");
            Console.WriteLine($@"Angle3 = {ang3.RadiansToDegrees():G}");
            Console.WriteLine($@"Angle4 = {ang4.RadiansToDegrees():G}");
            Console.WriteLine();

            var rotor = c1.ToMultivector().Gp(u1.ToMultivector()) + c2.ToMultivector().Gp(u2.ToMultivector()) + c3.ToMultivector().Gp(u3.ToMultivector()) + c4.ToMultivector().Gp(u4.ToMultivector());
            rotor = rotor / rotor.Norm();

            //Make sure this is actually a versor
            Console.WriteLine($@"rotor = {rotor.TermsToText()}");
            Console.WriteLine($@"rotor gp inverse(rotor) = {rotor.Gp(rotor.Inverse()).TermsToText()}");
            Console.WriteLine();

            var cc1 = u1.ApplyRotor(rotor);
            var cc2 = u2.ApplyRotor(rotor);
            var cc3 = u3.ApplyRotor(rotor);
            var cc4 = u4.ApplyRotor(rotor);

            Console.WriteLine($@"cc1 = {cc1.TermsToText()}");
            Console.WriteLine($@"cc2 = {cc2.TermsToText()}");
            Console.WriteLine($@"cc3 = {cc3.TermsToText()}");
            Console.WriteLine($@"cc4 = {cc4.TermsToText()}");
            Console.WriteLine();

            Console.WriteLine($@"c1 - cc1 = {(c1 - cc1).TermsToText()}");
            Console.WriteLine($@"c2 - cc2 = {(c2 - cc2).TermsToText()}");
            Console.WriteLine($@"c3 - cc3 = {(c3 - cc3).TermsToText()}");
            Console.WriteLine($@"c4 - cc4 = {(c4 - cc4).TermsToText()}");
            Console.WriteLine();
        }
    }
}