using System.Text;
using System.Collections.Generic;
using System.Linq;

namespace Ink
{
    public partial class InkParser
    {
<<<<<<< Updated upstream
        protected Parsed.Tag Tag ()
=======
        protected Parsed.Object StartTag ()
>>>>>>> Stashed changes
        {
            Whitespace ();

            if (ParseString ("#") == null)
                return null;

<<<<<<< Updated upstream
            Whitespace ();

            var sb = new StringBuilder ();
            do {
                // Read up to another #, end of input or newline
                string tagText = ParseUntilCharactersFromCharSet (_endOfTagCharSet);
                sb.Append (tagText);

                // Escape character
                if (ParseString ("\\") != null) {
                    char c = ParseSingleCharacter ();
                    if( c != (char)0 ) sb.Append(c);
                    continue;
                }

                break;
            } while ( true );

            var fullTagText = sb.ToString ().Trim();

            return new Parsed.Tag (new Runtime.Tag (fullTagText));
        }

        protected List<Parsed.Tag> Tags ()
        {
            var tags = OneOrMore (Tag);
            if (tags == null) return null;

            return tags.Cast<Parsed.Tag>().ToList();
        }

        CharacterSet _endOfTagCharSet = new CharacterSet ("#\n\r\\");
    }
}
=======
            if( parsingStringExpression ) {
                Error("Tags aren't allowed inside of strings. Please use \\# if you want a hash symbol.");
                // but allow us to continue anyway...
            }

            var result = (Parsed.Object)null;

            // End previously active tag before starting new one
            if( tagActive ) {
                var contentList = new Parsed.ContentList();
                contentList.AddContent(new Parsed.Tag { isStart = false });
                contentList.AddContent(new Parsed.Tag { isStart = true });
                result = contentList;
            }
            
            // Otherwise, just start a tag, no need for a content list
            else {
                result = new Parsed.Tag { isStart = true };
            }

            tagActive = true;

            Whitespace ();
            
            return result;
        }

        protected void EndTagIfNecessary(List<Parsed.Object> outputContentList)
        {
            if( tagActive ) {
                if( outputContentList != null )
                    outputContentList.Add(new Parsed.Tag { isStart = false });
                tagActive = false;
            }
        }

        protected void EndTagIfNecessary(Parsed.ContentList outputContentList)
        {
            if( tagActive ) {
                if( outputContentList != null )
                    outputContentList.AddContent(new Parsed.Tag { isStart = false });
                tagActive = false;
            }
        }
    }
    }

>>>>>>> Stashed changes

