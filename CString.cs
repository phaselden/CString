using System.Text;

namespace StringUtils;

public class CString
{
	private readonly StringBuilder _sb = new();

	private CString(ReadOnlySpan<Char> value)
	{
		var lineIndex = -1;
		var minDepth = 100000;
		
		foreach (var line in value.EnumerateLines())
		{
			lineIndex++;
			if (lineIndex == 0)
			{
				if (!line.IsWhiteSpace())
				{
					_sb.Append(line);
				}
			}
			else  // find the minimum indent of lines 2+
			{
				var depth  = GetIndentDepth(line);
				if (minDepth > depth)
					minDepth = depth;
			}
		}

		// If there is only 1 line, we are finished
		if (lineIndex == 0)
		{
			return;
		}
		
		// Remove leading spaces/tabs of 2nd and subsequent lines
		lineIndex = -1;
		foreach (var line in value.EnumerateLines())
		{
			lineIndex++;
			if (lineIndex == 0)  //ignore first line
				continue;
			
			if (_sb.Length > 0)
				_sb.AppendLine();
			_sb.Append(line[minDepth..]);
		}
		
		
		/*
		 // An String-based version of the parsing. Not 100% correct ATM. For example, it adds a newline at end
		 
		 var lines = value.Split(new [] { "\r\n" }, StringSplitOptions.None);
		if (lines.Length == 1)
		{
			_value = value;
			return;
		}

		// look for the indent size from the 2nd line on
		var minIndent = 1000;
		for (int i = 1; i < lines.Length; i++)
		{
			var lineIndent = GetIndentDepth(lines[i]);
			if (lineIndent < minIndent)
				minIndent = lineIndent;
		}
            
		//var sb = new StringBuilder();
		// First line
		if (!String.IsNullOrWhiteSpace(lines[0]))
		{
			sb.AppendLine(lines[0]);
		}
		// Second and subsequent lines
		for (int i = 1; i < lines.Length; i++)
		{
			if (String.IsNullOrWhiteSpace(lines[i]))
			{
				sb.AppendLine();
			}
			else
			{
				//var indent = FindIndent(lines[i]);
				sb.AppendLine(lines[i].Substring(minIndent));
			}
		}
		_value = sb.ToString();
		*/
	}

	private int GetIndentDepth(ReadOnlySpan<char> line)
	{
		for (var i = 0; i < line.Length; i++)
		{
			if (line[i] == '\t' || line[i] == ' ')
				continue;
			return i;
		}
		return 0;
	}

	public static implicit operator CString(string x) => new CString(x);
        
	public override string ToString()
	{
		return _sb.ToString();
	}
}
