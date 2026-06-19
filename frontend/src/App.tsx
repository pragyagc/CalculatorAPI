import { useState } from "react";
import { CalculatorService } from "./services/services/CalculatorService";

function App() {
  const [result, setResult] = useState<string>("");
  const [history, setHistory] = useState<any[]>([]);

  const handleSubmit = async (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();
    const formData = new FormData(e.currentTarget);

    const payload = {
      a: Number(formData.get("number1")),
      b: Number(formData.get("number2")),
      operation: formData.get("operation") as string,
    };

    try {
      const response = await CalculatorService.postApiCalculatorCalculate(payload);
      setResult(response.result ?? JSON.stringify(response));

      // Fetch history after calculation
      const historyData = await CalculatorService.getApiCalculatorHistory();
      setHistory(historyData);
      console.log(historyData);
    } catch (err) {
      setResult("Error: " + (err as Error).message);
    }
  };

  return (
    <div>
      <form onSubmit={handleSubmit}>
        <label>Number 1:</label>
        <input type="number" name="number1" /><br /><br />

        <label>Number 2:</label>
        <input type="number" name="number2" /><br /><br />

        <label>Operation:</label>
        <input type="text" name="operation" /><br /><br />

        <button type="submit">Calculate</button>
      </form>
      <p>Result: {result}</p>

      {/* ✅ Render history here */}
      <h2>Calculation History</h2>
      <ul>
        {history.map((item, index) => (
          <li key={index}>
            {item.id} | {item.operandA} {item.operator} {item.operandB} = {item.result}
          </li>
        ))}
      </ul>
    </div>
  );
}

export default App;
