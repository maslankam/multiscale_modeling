using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Model{
    public class CsvSpaceFormatter
    {
        // x, y, id, phase
        public string Format(CelluralSpace space)
        {
            var stringBuilder = new StringBuilder();

            for(int i = 0; i < space.GetXLength(); i++)
            {
                for(int j = 0; j < space.GetYLength(); j++){
                    var element = space.GetCell(i,j)?.MicroelementMembership;
                    var id = element?.Id.ToString() ?? "null";
                    var phase = element?.Phase.ToString() ?? "null";
                    stringBuilder.AppendLine($"{i}, {j}, {id}, {phase}");
                }
            }
            return stringBuilder.ToString();
        }

    }
}
