using Rgba32 = ImageFilters.Core.Imager.sPixel;
using static ImageFilters.Core.Imager.sPixel;

namespace ImageFilters.Core.Imager.Filters;

internal static class libKreed_
{
	// used for 2xSaI, Super Eagle, Super 2xSaI
	// using thresholds when comparing (Hawkynt)
	private static int _Conc2D(Rgba32 c00, Rgba32 c01, Rgba32 c10, Rgba32 c11)
	{
		var result = 0;

		var acAreAlike = c00.IsLike(c10);
		var x = acAreAlike ? 1 : 0;
		var y = c01.IsLike(c10) && !acAreAlike ? 1 : 0;

		var adAreAlike = c00.IsLike(c11);
		x += adAreAlike ? 1 : 0;
		y += c01.IsLike(c11) && !adAreAlike ? 1 : 0;

		if (x <= 1)
			result++;
		if (y <= 1)
			result--;

		return result;
	}

	// TODO: to be really exact, the comparisons are not that right by comparing to already interpolated values
	// TODO: when interpolating 3 or more points I'm using already calculated interpolations and weight them further
	//       which is not the mathematically correct approach, but it's enough - at least for now
	/// <summary>
	/// Kreed's SuperEagle modified by Hawkynt to allow thresholds
	/// </summary>
	public static void SuperEagle(IPixelWorker<Rgba32> worker)
	{
		var c0 = worker.SourceM1M1();
		var c1 = worker.SourceP0M1();
		var c2 = worker.SourceP1M1();
		var c3 = worker.SourceM1P0();
		var c4 = worker.SourceP0P0();
		var c5 = worker.SourceP1P0();
		var d4 = worker.SourceP2P0();
		var c6 = worker.SourceM1P1();
		var c7 = worker.SourceP0P1();
		var c8 = worker.SourceP1P1();
		var d5 = worker.SourceP2P1();
		var d1 = worker.SourceP0P2();
		var d2 = worker.SourceP1P2();

		Rgba32 e00 = c4, e11 = c4;
		Rgba32 e01, e10;
		if (c4.IsLike(c8))
		{
			var c48 = Interpolate(c4, c8);
			if (c7.IsLike(c5))
			{
				var c57 = Interpolate(c5, c7);
				var conc2D = 0;
				conc2D += _Conc2D(c57, c48, c6, d1);
				conc2D += _Conc2D(c57, c48, c3, c1);
				conc2D += _Conc2D(c57, c48, d2, d5);
				conc2D += _Conc2D(c57, c48, c2, d4);

				if (conc2D > 0)
				{
					e10 = c57;
					e01 = c57;
					e00 = e11 = Interpolate(c48, c57);
				}
				else if (conc2D < 0)
				{
					e01 = e10 = Interpolate(c48, c57);
				}
				else
				{
					e01 = e10 = c57;
				}
			}
			else
			{
				if (c48.IsLike(c1) && c48.IsLike(d5))
					e01 = Interpolate(Interpolate(c48, c1, d5), c5, 3, 1);
				else if (c48.IsLike(c1))
					e01 = Interpolate(Interpolate(c48, c1), c5, 3, 1);
				else if (c48.IsLike(d5))
					e01 = Interpolate(Interpolate(c48, d5), c5, 3, 1);
				else
					e01 = Interpolate(c48, c5);

				if (c48.IsLike(d2) && c48.IsLike(c3))
					e10 = Interpolate(Interpolate(c48, d2, c3), c7, 3, 1);
				else if (c48.IsLike(d2))
					e10 = Interpolate(Interpolate(c48, d2), c7, 3, 1);
				else if (c48.IsLike(c3))
					e10 = Interpolate(Interpolate(c48, c3), c7, 3, 1);
				else
					e10 = Interpolate(c48, c7);

			}
		}
		else
		{
			if (c7.IsLike(c5))
			{
				var c57 = Interpolate(c5, c7);
				e01 = c57;
				e10 = c57;

				if (c57.IsLike(c6) && c57.IsLike(c2))
					e00 = Interpolate(Interpolate(c57, c6, c2), c4, 3, 1);
				else if (c57.IsLike(c6))
					e00 = Interpolate(Interpolate(c57, c6), c4, 3, 1);
				else if (c57.IsLike(c2))
					e00 = Interpolate(Interpolate(c57, c2), c4, 3, 1);
				else
					e00 = Interpolate(c57, c4);

				if (c57.IsLike(d4) && c57.IsLike(d1))
					e11 = Interpolate(Interpolate(c57, d4, d1), c8, 3, 1);
				else if (c57.IsLike(d4))
					e11 = Interpolate(Interpolate(c57, d4), c8, 3, 1);
				else if (c57.IsLike(d1))
					e11 = Interpolate(Interpolate(c57, d1), c8, 3, 1);
				else
					e11 = Interpolate(c57, c8);

			}
			else
			{
				e11 = Interpolate(c8, c7, c5, 6, 1, 1);
				e00 = Interpolate(c4, c7, c5, 6, 1, 1);
				e10 = Interpolate(c7, c4, c8, 6, 1, 1);
				e01 = Interpolate(c5, c4, c8, 6, 1, 1);
			}
		}

		worker.TargetP0P0(e00);
		worker.TargetP1P0(e01);
		worker.TargetP0P1(e10);
		worker.TargetP1P1(e11);
	}

	/// <summary>
	/// Derek Liauw Kie Fa's 2XSaI
	/// </summary>
	public static void SaI2X(IPixelWorker<Rgba32> worker)
	{

		/*
		 * I E F J
		 * G A B K
		 * H C D L
		 * M N O P
		 */

		var I = worker.SourceM1M1();
		var E = worker.SourceP0M1();
		var F = worker.SourceP1M1();
		var J = worker.SourceP2M1();

		var G = worker.SourceM1P0();
		var A = worker.SourceP0P0();
		var B = worker.SourceP1P0();
		var K = worker.SourceP2P0();

		var H = worker.SourceM1P1();
		var C = worker.SourceP0P1();
		var D = worker.SourceP1P1();
		var L = worker.SourceP2P1();

		var M = worker.SourceM1P2();
		var N = worker.SourceP0P2();
		var O = worker.SourceP1P2();

		Rgba32 E00, E01, E10, E11;
		E00 = E01 = E10 = E11 = A;

		if (!A.IsLike(D) && B.IsLike(C))
		{
			var c57 = Interpolate(B, C);

			if (c57.IsLike(F) && A.IsLike(H) || c57.IsLike(E) && c57.IsLike(D) && !A.IsLike(F) && A.IsLike(I))
				E01 = c57;
			else
				E01 = Interpolate(A, c57);

			if (c57.IsLike(H) && A.IsLike(F) || c57.IsLike(G) && c57.IsLike(D) && !A.IsLike(H) && A.IsLike(I))
				E10 = c57;
			else
				E10 = Interpolate(A, c57);

			E11 = c57;
		}

		else if (A.IsLike(D) && !B.IsLike(C))
		{
			var c48 = Interpolate(A, D);
			if ((!c48.IsLike(E) || !B.IsLike(L)) && (!c48.IsLike(C) || !c48.IsLike(F) || B.IsLike(E) || !B.IsLike(J)))
				E01 = Interpolate(c48, B);

			if ((!c48.IsLike(G) || !C.IsLike(O)) && (!c48.IsLike(B) || !c48.IsLike(H) || G.IsLike(C) || !C.IsLike(M)))
				E10 = Interpolate(c48, C);
		}

		else if (A.IsLike(D) && B.IsLike(C))
		{
			var c48 = Interpolate(A, D);
			var c57 = Interpolate(B, C);
			if (!c48.IsLike(c57))
			{
				var conc2D = 0;
				conc2D += _Conc2D(c48, c57, G, E);
				conc2D -= _Conc2D(c57, c48, K, F);
				conc2D -= _Conc2D(c57, c48, H, N);
				conc2D += _Conc2D(c48, c57, L, O);

				if (conc2D < 0) E11 = c57;
				else if (conc2D == 0) E11 = Interpolate(c48, c57);

				E10 = Interpolate(c48, c57);
				E01 = Interpolate(c48, c57);
			}
		}
		else
		{
			E11 = Interpolate(A, B, C, D);

			if (!A.IsLike(C) || !A.IsLike(F) || B.IsLike(E) || !B.IsLike(J))
				E01 = B.IsLike(E) && B.IsLike(D) && !A.IsLike(F) && A.IsLike(I)
					? Interpolate(B, E, D)
					: Interpolate(A, B);


			if (!A.IsLike(B) || !A.IsLike(H) || G.IsLike(C) || !C.IsLike(M))
				E10 = C.IsLike(G) && C.IsLike(D) && !A.IsLike(H) && A.IsLike(I)
					? Interpolate(C, G, D)
					: Interpolate(A, C);
		}

		//if (worker.SourceX() == 51 && worker.SourceY() == 31)
		//{
		//	var a_ = 0;
		//}

		worker.TargetP0P0(E00);
		worker.TargetP1P0(E01);
		worker.TargetP0P1(E10);
		worker.TargetP1P1(E11);
	}

	/// <summary>
	/// Kreed's SuperSaI
	/// </summary>
	public static void SuperSaI(IPixelWorker<Rgba32> worker)
	{
		/*
        * I E F J
        * G A B K
		* H C D L
        * M N O P
        */

		var I = worker.SourceM1M1();
		var E = worker.SourceP0M1();
		var F = worker.SourceP1M1();
		var J = worker.SourceP2M1();

		var G = worker.SourceM1P0();
		var A = worker.SourceP0P0();
		var B = worker.SourceP1P0();
		var K = worker.SourceP2P0();

		var H = worker.SourceM1P1();
		var C = worker.SourceP0P1();
		var D = worker.SourceP1P1();
		var L = worker.SourceP2P1();

		var M = worker.SourceM1P2();
		var N = worker.SourceP0P2();
		var O = worker.SourceP1P2();
		var P = worker.SourceP2P2();

		Rgba32 E01, E10, E11;
		var E00 = E01 = E11 = A;

		if (!C.IsLike(B) || A.IsLike(D))
		{
			if (!A.IsLike(D) || C.IsLike(B))
			{
				if (A.IsLike(D) && C.IsLike(B))
				{
					var CB = Interpolate(C, B);
					var AD = Interpolate(A, D);
					var conc2D = 0;
					conc2D += _Conc2D(CB, AD, H, N);
					conc2D += _Conc2D(CB, AD, G, E);
					conc2D += _Conc2D(CB, AD, O, L);
					conc2D += _Conc2D(CB, AD, F, K);

					if (conc2D > 0)
						E11 = E01 = CB;

					else if (conc2D == 0)
					{
						E11 = Interpolate(AD, CB);
						E01 = Interpolate(AD, CB);
					}
				}
				else
				{
					if (D.IsLike(B) && D.IsLike(N) && C.IsNotLike(O) && D.IsNotLike(M))
						E11 = Interpolate(Interpolate(D, B, N), C, 3, 1);

					else if (C.IsLike(A) && C.IsLike(O) && C.IsNotLike(P) && D.IsNotLike(N))
						E11 = Interpolate(Interpolate(C, A, O), D, 3, 1);

					else
						E11 = Interpolate(C, D);

					if (B.IsLike(D) && B.IsLike(E) && B.IsNotLike(I) && A.IsNotLike(F))
						E01 = Interpolate(Interpolate(B, D, E), A, 3, 1);

					else if (A.IsLike(C) && A.IsLike(F) && B.IsNotLike(E) && A.IsNotLike(J))
						E01 = Interpolate(Interpolate(A, C, F), B, 3, 1);

					else
						E01 = Interpolate(A, B);
				}
			}

		}
		else
		{
			var c57 = Interpolate(C, B);
			E11 = c57;
			E01 = c57;
		}

		if (A.IsLike(D) && A.IsLike(G) && C.IsNotLike(B) && A.IsNotLike(O))
			E10 = Interpolate(C, Interpolate(A, D, G));

		else if (A.IsLike(H) && A.IsLike(B) && C.IsNotLike(G) && A.IsNotLike(M))
			E10 = Interpolate(C, Interpolate(A, H, B));

		else
			E10 = C;

		if (C.IsLike(B) && C.IsLike(H) && A.IsNotLike(D) && C.IsNotLike(F))
			E00 = Interpolate(Interpolate(C, B, H), A);

		else if (C.IsLike(G) && C.IsLike(D) && A.IsNotLike(H) && C.IsNotLike(I))
			E00 = Interpolate(Interpolate(C, G, D), A);

		worker.TargetP0P0(E00);
		worker.TargetP1P0(E01);
		worker.TargetP0P1(E10);
		worker.TargetP1P1(E11);
	}


} // end class
  // end namespace
