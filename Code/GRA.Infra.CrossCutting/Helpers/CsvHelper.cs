using System.Data;
using System.Text;

namespace GRA.Infra.CrossCutting.Helpers
{
    public static class CsvHelper
    {
        /// <summary>
        /// Import CSV file.
        /// </summary>
        /// <param name="csvFilePath"></param>
        public static CsvHelperResponse GetDataFromCsvFile(string csvFilePath)
        {
            if (string.IsNullOrEmpty(csvFilePath))
                return new CsvHelperResponse("Caminho informado está inválido.");

            else if (!File.Exists(csvFilePath))
                return new CsvHelperResponse($"Não foi encontrado arquivo com o diretório \"{csvFilePath}\".");

            DataTable dataTable = new DataTable();

            IEnumerable<string> allContextRead = File.ReadLines(csvFilePath, encoding: Encoding.UTF8);

            if (allContextRead == null)
                return new CsvHelperResponse($"Não foi informações de leitura no arquivo \"{csvFilePath}\".");
            
            // Read first line as Header.
            string[]? columnNames = allContextRead?.First()?.Split(';');

            if (columnNames == null)
                return new CsvHelperResponse("Leitura da primeira linha como coluna, está inválida.");

            foreach (string columnName in columnNames)
                dataTable.Columns.Add(columnName.Trim());

            // Read the lines as Content.
            string[] lines = allContextRead!.ToArray();

            for (int i = 1; i < lines.Length; i++)
            {
                string[]? fields = lines[i]?.Split(';');
                if (fields == null)
                    continue;

                DataRow row = dataTable.NewRow();

                for (int j = 0; j < fields.Length; j++)
                    row[j] = fields[j].Trim();

                dataTable.Rows.Add(row);
            }

            return new CsvHelperResponse(dataTable);
        }

        public class CsvHelperResponse
        {
            public bool Success { get; private set; } = true;

            private string? _messageError;
            public string? MessageError
            {
                get { return _messageError; }
                set
                {
                    if (!string.IsNullOrEmpty(value))
                        this.Success = false;

                    _messageError = value;
                }
            }

            public DataTable? DataTable { get; private set; }

            public CsvHelperResponse(DataTable dataTable)
            {
                this.DataTable = dataTable;
            }

            public CsvHelperResponse(string errorMessage)
            {
                this.MessageError = errorMessage;
            }
        }
    }
}
