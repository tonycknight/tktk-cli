using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Tk.Toolkit.Cli.Waffle
{
    [ExcludeFromCodeCoverage]
    // Original source: https://www.red-gate.com/simple-talk/wp-content/uploads/imported/465-WaffleEngine.cs.txt
    internal class WaffleGenerator
    {
        private readonly IRng _rng;
        private string _title = "";
        private int _cardinalSequence;
        private int _ordinalSequence;

        public WaffleGenerator(IRng rng) => _rng = rng;

        public WaffleGenerator() : this(new Rng()) { }

        public StringBuilder Generate(int paragraphs, bool addHeader)
        {
            var result = new StringBuilder();

            _title = string.Empty;
            _cardinalSequence = 0;
            _ordinalSequence = 0;
            if (addHeader)
            {
                var title = new StringBuilder();
                EvaluatePhrase("the |o of |2 |o", title);

                _title = TitleCaseWords(title.ToString());

                result.AppendLine(_title);
                result.AppendLine();
                EvaluatePhrase("\"|A |B |C |t\"\n", result);
                EvaluatePhrase("(|f |s in The Journal of the |uc (|uy))", result);
                result.AppendLine();
                EvaluatePhrase("|c.", result);
                result.AppendLine();
            }


            for (int i = 0; i < paragraphs; i++)
            {
                if (i != 0)
                {
                    EvaluateRandomPhrase(Waffles.MaybeHeading, result);
                }

                EvaluatePhrase("|A |B |C |D.  ", result);
                EvaluateRandomPhrase(Waffles.MaybeParagraph, result);
            }

            return result;
        }

        private StringBuilder EvaluatePhrase(string phrase, StringBuilder accum)
        {
            for (int i = 0; i < phrase.Length; i++)
            {
                if (phrase[i] == '|' && i + 1 < phrase.Length)
                {
                    i++;

                    StringBuilder escape = accum;
                    bool titleCase = false;

                    if (phrase[i] == 'u' && i + 1 < phrase.Length)
                    {
                        escape = new StringBuilder();
                        titleCase = true;
                        i++;
                    }

                    switch (phrase[i])
                    {
                        case 'a':
                            EvaluateCardinalSequence(escape);
                            break;
                        case 'b':
                            EvaluateOrdinalSequence(escape);
                            break;
                        case 'c':
                            EvaluateRandomPhrase(Waffles.Buzzphrases, escape);
                            break;
                        case 'd':
                            EvaluateRandomPhrase(Waffles.Verbs, escape);
                            break;
                        case 'e':
                            EvaluateRandomPhrase(Waffles.Adverbs, escape);
                            break;
                        case 'f':
                            EvaluateRandomPhrase(Waffles.Forenames, escape);
                            break;
                        case 's':
                            EvaluateRandomPhrase(Waffles.Surnames, escape);
                            break;
                        case 'o':
                            EvaluateRandomPhrase(Waffles.ArtyNouns, escape);
                            break;
                        case 'y':
                            RandomDate(escape);
                            break;
                        case 'h':
                            EvaluateRandomPhrase(Waffles.Prefixes, escape);
                            break;
                        case 'A':
                            EvaluateRandomPhrase(Waffles.PreamblePhrases, escape);
                            break;
                        case 'B':
                            EvaluateRandomPhrase(Waffles.SubjectPhrases, escape);
                            break;
                        case 'C':
                            EvaluateRandomPhrase(Waffles.VerbPhrases, escape);
                            break;
                        case 'D':
                            EvaluateRandomPhrase(Waffles.ObjectPhrases, escape);
                            break;
                        case '1':
                            EvaluateRandomPhrase(Waffles.FirstAdjectivePhrases, escape);
                            break;
                        case '2':
                            EvaluateRandomPhrase(Waffles.SecondAdjectivePhrases, escape);
                            break;
                        case '3':
                            EvaluateRandomPhrase(Waffles.NounPhrases, escape);
                            break;
                        case '4':
                            EvaluateRandomPhrase(Waffles.Cliches, escape);
                            break;
                        case 't':
                            escape.Append(_title);
                            break;
                        case 'n':
                            escape.Append("</p>\n<p>");
                            break;
                    }

                    if (titleCase)
                    {
                        accum.Append(TitleCaseWords(escape.ToString()));
                    }
                }
                else
                {
                    accum.Append(phrase[i]);
                }
            }
            return accum;
        }

        private StringBuilder EvaluateRandomPhrase(string[] phrases, StringBuilder output)
        {
            var phrase = _rng.Pick(phrases);

            return EvaluatePhrase(phrase, output);
        }

        private void EvaluateCardinalSequence(StringBuilder output)
        {
            if (_cardinalSequence >= Waffles.CardinalSequence.Length)
            {
                _cardinalSequence = 0;
            }

            output.Append(Waffles.CardinalSequence[_cardinalSequence++]);
        }

        private void EvaluateOrdinalSequence(StringBuilder output)
        {
            if (_ordinalSequence >= Waffles.OrdinalSequences.Length)
            {
                _ordinalSequence = 0;
            }

            output.Append(Waffles.OrdinalSequences[_ordinalSequence++]);
        }

        private StringBuilder RandomDate(StringBuilder output)
        {
            output.Append(_rng.PickDate().Year);
            return output;
        }

        private static string TitleCaseWords(string input) => System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(input);
    }
}
