namespace HtmlLiveEditor.Config
{
    public static class Snippets
    {
        public const string Boilerplate = @"<!DOCTYPE html>
<html lang=""en"">
<head>
    <meta charset=""UTF-8"">
    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
    <title>Document</title>
    <style>
        body { font-family: system-ui, sans-serif; margin: 2rem; }
    </style>
</head>
<body>
    <h1>Hello World</h1>
    <p>Start building here...</p>
</body>
</html>";

        public const string Flexbox = @"<div style=""display: flex; gap: 1rem; align-items: center; justify-content: center; padding: 2rem;"">
    <div style=""flex: 1; padding: 1rem; background: #f0f0f0; border-radius: 8px;"">Column 1</div>
    <div style=""flex: 1; padding: 1rem; background: #e0e0e0; border-radius: 8px;"">Column 2</div>
    <div style=""flex: 1; padding: 1rem; background: #d0d0d0; border-radius: 8px;"">Column 3</div>
</div>";

        public const string Grid = @"<div style=""display: grid; grid-template-columns: repeat(3, 1fr); gap: 1rem; padding: 2rem;"">
    <div style=""padding: 1rem; background: #f0f0f0; border-radius: 8px;"">Cell 1</div>
    <div style=""padding: 1rem; background: #e0e0e0; border-radius: 8px;"">Cell 2</div>
    <div style=""padding: 1rem; background: #d0d0d0; border-radius: 8px;"">Cell 3</div>
    <div style=""padding: 1rem; background: #c0c0c0; border-radius: 8px;"">Cell 4</div>
    <div style=""padding: 1rem; background: #b0b0b0; border-radius: 8px;"">Cell 5</div>
    <div style=""padding: 1rem; background: #a0a0a0; border-radius: 8px;"">Cell 6</div>
</div>";

        public const string Form = @"<form style=""max-width: 400px; margin: 2rem auto; font-family: system-ui;"">
    <div style=""margin-bottom: 1rem;"">
        <label for=""name"" style=""display: block; margin-bottom: 0.25rem; font-weight: 600;"">Name</label>
        <input type=""text"" id=""name"" placeholder=""Enter your name"" style=""width: 100%; padding: 0.5rem; border: 1px solid #ccc; border-radius: 4px;"" />
    </div>
    <div style=""margin-bottom: 1rem;"">
        <label for=""email"" style=""display: block; margin-bottom: 0.25rem; font-weight: 600;"">Email</label>
        <input type=""email"" id=""email"" placeholder=""Enter your email"" style=""width: 100%; padding: 0.5rem; border: 1px solid #ccc; border-radius: 4px;"" />
    </div>
    <div style=""margin-bottom: 1rem;"">
        <label for=""message"" style=""display: block; margin-bottom: 0.25rem; font-weight: 600;"">Message</label>
        <textarea id=""message"" rows=""4"" placeholder=""Your message..."" style=""width: 100%; padding: 0.5rem; border: 1px solid #ccc; border-radius: 4px;""></textarea>
    </div>
    <button type=""submit"" style=""padding: 0.5rem 1.5rem; background: #007bff; color: white; border: none; border-radius: 4px; cursor: pointer;"">Submit</button>
</form>";

        public const string Table = @"<table style=""border-collapse: collapse; width: 100%; font-family: system-ui;"">
    <thead>
        <tr style=""background: #f8f9fa;"">
            <th style=""padding: 0.75rem; border: 1px solid #dee2e6; text-align: left;"">Name</th>
            <th style=""padding: 0.75rem; border: 1px solid #dee2e6; text-align: left;"">Email</th>
            <th style=""padding: 0.75rem; border: 1px solid #dee2e6; text-align: left;"">Role</th>
        </tr>
    </thead>
    <tbody>
        <tr>
            <td style=""padding: 0.75rem; border: 1px solid #dee2e6;"">Alice</td>
            <td style=""padding: 0.75rem; border: 1px solid #dee2e6;"">alice@example.com</td>
            <td style=""padding: 0.75rem; border: 1px solid #dee2e6;"">Admin</td>
        </tr>
        <tr style=""background: #f8f9fa;"">
            <td style=""padding: 0.75rem; border: 1px solid #dee2e6;"">Bob</td>
            <td style=""padding: 0.75rem; border: 1px solid #dee2e6;"">bob@example.com</td>
            <td style=""padding: 0.75rem; border: 1px solid #dee2e6;"">User</td>
        </tr>
    </tbody>
</table>";
    }
}
