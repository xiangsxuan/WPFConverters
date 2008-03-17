using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using NUnit.Framework;
using Kent.Boogaart.Converters;

namespace Kent.Boogaart.Converters.UnitTest
{
	[TestFixture]
	public sealed class CaseConverterTest : UnitTest
	{
		private CaseConverter _caseConverter;

		protected override void SetUpCore()
		{
			base.SetUpCore();
			_caseConverter = new CaseConverter();
		}

		[Test]
		public void Constructor_ShouldSetDefaults()
		{
			Assert.AreEqual(CharacterCasing.Normal, _caseConverter.Casing);
		}

		[Test]
		public void Constructor_Casing_ShouldSetCasing()
		{
			_caseConverter = new CaseConverter(CharacterCasing.Upper);
			Assert.AreEqual(CharacterCasing.Upper, _caseConverter.Casing);
			_caseConverter = new CaseConverter(CharacterCasing.Lower);
			Assert.AreEqual(CharacterCasing.Lower, _caseConverter.Casing);
		}

		[Test]
		[ExpectedException(typeof(ArgumentException), ExpectedMessage = "'100' is not a valid value for property 'Casing'.")]
		public void Casing_ShouldThrowIfInvalid()
		{
			_caseConverter.Casing = (CharacterCasing) 100;
		}

		[Test]
		public void Casing_ShouldGetAndSetCasing()
		{
			Assert.AreEqual(CharacterCasing.Normal, _caseConverter.Casing);
			_caseConverter.Casing = CharacterCasing.Upper;
			Assert.AreEqual(CharacterCasing.Upper, _caseConverter.Casing);
			_caseConverter.Casing = CharacterCasing.Lower;
			Assert.AreEqual(CharacterCasing.Lower, _caseConverter.Casing);
		}

		[Test]
		public void Convert_ShouldDoNothingIfValueIsNotAString()
		{
			Assert.AreSame(DependencyProperty.UnsetValue, _caseConverter.Convert(123, null, null, null));
			Assert.AreSame(DependencyProperty.UnsetValue, _caseConverter.Convert(123d, null, null, null));
			Assert.AreSame(DependencyProperty.UnsetValue, _caseConverter.Convert(DateTime.Now, null, null, null));
		}

		[Test]
		public void Convert_ShouldDoNothingIfCasingIsNormal()
		{
			Assert.AreEqual(CharacterCasing.Normal, _caseConverter.Casing);
			Assert.AreEqual("abcd", _caseConverter.Convert("abcd", null, null, null));
			Assert.AreEqual("ABCD", _caseConverter.Convert("ABCD", null, null, null));
			Assert.AreEqual("AbCd", _caseConverter.Convert("AbCd", null, null, null));
		}

		[Test]
		public void Convert_ShouldConvertStringsToSpecifiedCasing()
		{
			_caseConverter.Casing = CharacterCasing.Lower;
			Assert.AreEqual("abcd", _caseConverter.Convert("abcd", null, null, null));
			Assert.AreEqual("abcd", _caseConverter.Convert("ABCD", null, null, null));
			Assert.AreEqual("abcd", _caseConverter.Convert("AbCd", null, null, null));

			_caseConverter.Casing = CharacterCasing.Upper;
			Assert.AreEqual("ABCD", _caseConverter.Convert("abcd", null, null, null));
			Assert.AreEqual("ABCD", _caseConverter.Convert("ABCD", null, null, null));
			Assert.AreEqual("ABCD", _caseConverter.Convert("AbCd", null, null, null));
		}

		[Test]
		public void Convert_ShouldUseSpecifiedCulture()
		{
			CultureInfo cultureInfo = new CultureInfo("tr");

			_caseConverter.Casing = CharacterCasing.Lower;
			Assert.AreEqual("ijk", _caseConverter.Convert("ijk", null, null, cultureInfo));
			Assert.AreEqual("ıjk", _caseConverter.Convert("IJK", null, null, cultureInfo));
			Assert.AreEqual("ijk", _caseConverter.Convert("iJk", null, null, cultureInfo));

			_caseConverter.Casing = CharacterCasing.Upper;
			Assert.AreEqual("İJK", _caseConverter.Convert("ijk", null, null, cultureInfo));
			Assert.AreEqual("IJK", _caseConverter.Convert("IJK", null, null, cultureInfo));
			Assert.AreEqual("İJK", _caseConverter.Convert("iJk", null, null, cultureInfo));
		}

		[Test]
		public void ConvertBack_ShouldReturnUnsetValue()
		{
			Assert.AreSame(DependencyProperty.UnsetValue, _caseConverter.ConvertBack(null, null, null, null));
			Assert.AreSame(DependencyProperty.UnsetValue, _caseConverter.ConvertBack(123, null, null, null));
			Assert.AreSame(DependencyProperty.UnsetValue, _caseConverter.ConvertBack(DateTime.Now, null, null, null));
			Assert.AreSame(DependencyProperty.UnsetValue, _caseConverter.ConvertBack("abc", null, null, null));
		}
	}
}
