using System;
using System.Globalization;
using System.Linq;
using GAPoTNumLib.GAPoT;

namespace GAPoTNumLib.Framework.Samples
{
    public static class ClarkeRotation4DSample
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
            
            //Define voltage vector basis e1, e2, e3
            var e1 = u2 - u1;
            var e2 = u3 - u1;
            var e3 = u4 - u1;
            var e123 = GaPoTNumUtils.OuterProduct(e1, e2, e3);
            
            Console.WriteLine($@"e1 = {e1.TermsToText()}");
            Console.WriteLine($@"e2 = {e2.TermsToText()}");
            Console.WriteLine($@"e3 = {e3.TermsToText()}");
            Console.WriteLine($@"e123 = {e123.TermsToText()}");
            Console.WriteLine();
            
            //Ortho-normalize e1, e2, e3
            var orthoVectors = new[] {e1, e2, e3}.ApplyGramSchmidt(true).ToArray();

            var c1 = orthoVectors[0];
            var c2 = orthoVectors[1];
            var c3 = orthoVectors[2];
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

            //Find a simple rotor to get c1 from u1
            var rotor1 = u1.GetRotorToVector(c1);
            
            //Make sure this is a rotor
            Console.WriteLine($@"rotor1 = {rotor1.TermsToText()}");
            Console.WriteLine($@"reverse(rotor1) = {rotor1.Reverse().TermsToText()}");
            Console.WriteLine($@"rotor1 gp reverse(rotor1) = {rotor1.Gp(rotor1.Reverse()).TermsToText()}");
            Console.WriteLine();
            
            //Rotate all basis vectors using rotor1
            var u1_1 = u1.ApplyRotor(rotor1);
            var u2_1 = u2.ApplyRotor(rotor1);
            var u3_1 = u3.ApplyRotor(rotor1);
            var u4_1 = u4.ApplyRotor(rotor1);
            
            Console.WriteLine($@"rotation of u1 under rotor1 = {u1_1.TermsToText()}");
            Console.WriteLine($@"rotation of u2 under rotor1 = {u2_1.TermsToText()}");
            Console.WriteLine($@"rotation of u3 under rotor1 = {u3_1.TermsToText()}");
            Console.WriteLine($@"rotation of u4 under rotor1 = {u4_1.TermsToText()}");
            Console.WriteLine();
            
            //Find a simple rotor to get c2 from u2_1
            var rotor2 = u2_1.GetRotorToVector(c2);
            
            //Make sure this is a rotor
            Console.WriteLine($@"rotor2 = {rotor2.TermsToText()}");
            Console.WriteLine($@"reverse(rotor2) = {rotor2.Reverse().TermsToText()}");
            Console.WriteLine($@"rotor2 gp reverse(rotor2) = {rotor2.Gp(rotor2.Reverse()).TermsToText()}");
            Console.WriteLine();
            
            //We can combine the two simple rotors to get <c1, c2> from <u1, u2> directly
            var rotor21 = rotor2.Gp(rotor1);
            Console.WriteLine($@"rotor21 = {rotor21.TermsToText()}");
            Console.WriteLine($@"reverse(rotor21) = {rotor21.Reverse().TermsToText()}");
            Console.WriteLine($@"rotor21 gp reverse(rotor21) = {rotor21.Gp(rotor21.Reverse()).TermsToText()}");
            Console.WriteLine();
            
            Console.WriteLine($@"rotation of u1 under rotor21 = {u1.ApplyRotor(rotor21).TermsToText()}");
            Console.WriteLine($@"rotation of u2 under rotor21 = {u2.ApplyRotor(rotor21).TermsToText()}");
            Console.WriteLine($@"rotation of u3 under rotor21 = {u3.ApplyRotor(rotor21).TermsToText()}");
            Console.WriteLine($@"rotation of u4 under rotor21 = {u4.ApplyRotor(rotor21).TermsToText()}");
            Console.WriteLine();

            //Rotate all basis vectors using rotor2
            var u1_2 = u1_1.ApplyRotor(rotor2);
            var u2_2 = u2_1.ApplyRotor(rotor2);
            var u3_2 = u3_1.ApplyRotor(rotor2);
            var u4_2 = u4_1.ApplyRotor(rotor2);
            
            Console.WriteLine($@"rotation of u1_1 under rotor2 = {u1_2.TermsToText()}");
            Console.WriteLine($@"rotation of u2_1 under rotor2 = {u2_2.TermsToText()}");
            Console.WriteLine($@"rotation of u3_1 under rotor2 = {u3_2.TermsToText()}");
            Console.WriteLine($@"rotation of u4_1 under rotor2 = {u4_2.TermsToText()}");
            Console.WriteLine();
            
            //Find a simple rotor to get c3 from u3_2
            var rotor3 = u3_2.GetRotorToVector(c3);
            
            //Make sure this is a rotor
            Console.WriteLine($@"rotor3 = {rotor3.TermsToText()}");
            Console.WriteLine($@"reverse(rotor3) = {rotor3.Reverse().TermsToText()}");
            Console.WriteLine($@"rotor3 gp reverse(rotor3) = {rotor3.Gp(rotor3.Reverse()).TermsToText()}");
            Console.WriteLine();
            
            //Rotate all basis vectors using rotor2
            var u1_3 = u1_2.ApplyRotor(rotor3);
            var u2_3 = u2_2.ApplyRotor(rotor3);
            var u3_3 = u3_2.ApplyRotor(rotor3);
            var u4_3 = u4_2.ApplyRotor(rotor3);
            
            Console.WriteLine($@"rotation of u1_2 under rotor3 = {u1_3.TermsToText()}");
            Console.WriteLine($@"rotation of u2_2 under rotor3 = {u2_3.TermsToText()}");
            Console.WriteLine($@"rotation of u3_2 under rotor3 = {u3_3.TermsToText()}");
            Console.WriteLine($@"rotation of u4_2 under rotor3 = {u4_3.TermsToText()}");
            Console.WriteLine();
            
            //Compute final rotor as rotor3 gp rotor2 gp rotor1
            var rotor = rotor3.Gp(rotor2).Gp(rotor1);
            
            //Make sure this is a rotor
            Console.WriteLine($@"rotor = {rotor.TermsToText()}");
            Console.WriteLine($@"reverse(rotor) = {rotor.Reverse().TermsToText()}");
            Console.WriteLine($@"rotor gp reverse(rotor) = {rotor.Gp(rotor.Reverse()).TermsToText()}");
            Console.WriteLine();
            
            //Are the two rotors are orthogonal? No.
            //var diffRotors = rotor21.Gp(rotor3) - rotor3.Gp(rotor21);
            //Console.WriteLine($@"Are the two rotors are orthogonal (0 means Yes)? {diffRotors.TermsToText()}");
            //Console.WriteLine();

            //Rotate all original basis vectors using final rotor
            var cc1 = u1.ApplyRotor(rotor);
            var cc2 = u2.ApplyRotor(rotor);
            var cc3 = u3.ApplyRotor(rotor);
            var cc4 = u4.ApplyRotor(rotor);
            
            Console.WriteLine($@"rotation of u1 under rotor = {cc1.TermsToText()}");
            Console.WriteLine($@"rotation of u2 under rotor = {cc2.TermsToText()}");
            Console.WriteLine($@"rotation of u3 under rotor = {cc3.TermsToText()}");
            Console.WriteLine($@"rotation of u4 under rotor = {cc4.TermsToText()}");
            Console.WriteLine();
            
            //Find the projections of u1, u2, u3, u4 on e234
            var pu1 = u1.GetProjectionOnBlade(e123);
            var pu2 = u2.GetProjectionOnBlade(e123);
            var pu3 = u3.GetProjectionOnBlade(e123);
            var pu4 = u4.GetProjectionOnBlade(e123);
            
            Console.WriteLine($@"projection of u1 on e123 = {pu1.TermsToText()}");
            Console.WriteLine($@"projection of u2 on e123 = {pu2.TermsToText()}");
            Console.WriteLine($@"projection of u3 on e123 = {pu3.TermsToText()}");
            Console.WriteLine($@"projection of u4 on e123 = {pu4.TermsToText()}");
            Console.WriteLine();

            Console.WriteLine($@"angle between projections of u1,u2 on e123 = {pu1.GetAngle(pu2).RadiansToDegrees():G}");
            Console.WriteLine($@"angle between projections of u2,u3 on e123 = {pu2.GetAngle(pu3).RadiansToDegrees():G}");
            Console.WriteLine($@"angle between projections of u1,u3 on e123 = {pu1.GetAngle(pu3).RadiansToDegrees():G}");
            Console.WriteLine($@"angle between projections of u1,u4 on e123 = {pu1.GetAngle(pu4).RadiansToDegrees():G}");
            Console.WriteLine($@"angle between projections of u2,u4 on e123 = {pu2.GetAngle(pu4).RadiansToDegrees():G}");
            Console.WriteLine($@"angle between projections of u3,u4 on e123 = {pu3.GetAngle(pu4).RadiansToDegrees():G}");
            Console.WriteLine();
        }
    }
}