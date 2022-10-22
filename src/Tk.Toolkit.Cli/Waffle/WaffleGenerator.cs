using System.Text;

namespace Tk.Toolkit.Cli.Waffle
{
    // Original source: https://www.red-gate.com/simple-talk/wp-content/uploads/imported/465-WaffleEngine.cs.txt
    internal class WaffleGenerator
    {
        private readonly IRng _rng;
        public WaffleGenerator(IRng rng) => _rng = rng;

        public WaffleGenerator() : this(new Rng()) { }
        
        public StringBuilder Generate(int paragraphs, bool addHeader, RenderMode render) 
        {
            var result = new StringBuilder();
            var ctx = new GenContext(_rng)
            {
                Rendering = render,
            };

            if (addHeader)
            {
                var title = new StringBuilder();
                
                ctx.Title = title.EvaluatePhrase(ctx, "the |o of |2 |o")
                                 .ToString()
                                 .TitleCaseWords();

                result = result.AppendLine($"[h1]{ctx.Title}[/h1]".Render(ctx.Rendering))
                               .AppendLine()
                               .EvaluatePhrase(ctx, "[q]|A |B |C |t[/q]".Render(ctx.Rendering))
                               .EvaluatePhrase(ctx, "[br][b][i]|f |s in |p of the |uc (|uy)[/i][/b][p]".Render(ctx.Rendering))
                               .AppendLine("[p]".Render(ctx.Rendering))
                               .AppendLine()
                               .EvaluatePhrase(ctx, "[i]|c.[/i]".Render(ctx.Rendering))
                               .AppendLine("[p]".Render(ctx.Rendering))
                               .AppendLine();
            }


            for (int i = 0; i < paragraphs; i++)
            {
                if (i != 0)
                {
                    result = result.EvaluateRandomPhrase(ctx, Waffles.MaybeHeading);
                }

                result = result.EvaluatePhrase(ctx, "|A |B |C |D.  ")
                               .EvaluateRandomPhrase(ctx, Waffles.MaybeParagraph);
            }

            return result;
        }
    }
}
