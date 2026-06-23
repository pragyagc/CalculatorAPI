import { useEffect, useState } from "react";
import { CalculatorService } from "./services/services/CalculatorService";

type HistoryItem = {
  operandA: number;
  operandB: number;
  operator: string;
  result: number;
};

type HistoryResponse = {
  items: HistoryItem[];
  totalCount: number;
};

type CalculateResponse = {
  result: number;
};

function App() {
  const [a, setA] = useState("");
  const [b, setB] = useState("");
  const [operation, setOperation] = useState("+");

  const [result, setResult] = useState<number | null>(null);
  const [history, setHistory] = useState<HistoryItem[]>([]);
  const [loading, setLoading] = useState(false);

  // 1. Create session once
  useEffect(() => {
    CalculatorService.postApiCalculatorNewSession();
  }, []);

  // 2. Calculate
  const calculate = async () => {
    try {
      setLoading(true);

      const res = (await CalculatorService.postApiCalculatorCalculate({
        a: Number(a),
        b: Number(b),
        operation,
      })) as CalculateResponse;

      setResult(res.result);
    } catch (err) {
      console.error(err);
      alert("Calculation failed");
    } finally {
      setLoading(false);
    }
  };

  // 3. Load history 
  const loadHistory = async () => {
    try {
      const res = (await CalculatorService.getApiCalculatorHistory(
        1,
        10
      )) as HistoryResponse;

      setHistory(res.items);
    } catch (err) {
      console.error(err);
      alert("Failed to load history");
    }
  };

  // 4. New session
  const startNewSession = async () => {
    await CalculatorService.postApiCalculatorNewSession();
    setHistory([]);
    setResult(null);
    setA("");
    setB("");
  };

  return (
    <div style={{ maxWidth: 600, margin: "40px auto" }}>
      <h1>Calculator</h1>

      <input value={a} onChange={(e) => setA(e.target.value)} />
      <select value={operation} onChange={(e) => setOperation(e.target.value)}>
        <option>+</option>
        <option>-</option>
        <option>*</option>
        <option>/</option>
      </select>
      <input value={b} onChange={(e) => setB(e.target.value)} />

      <button onClick={calculate} disabled={loading}>
        {loading ? "..." : "Calculate"}
      </button>

      <button onClick={startNewSession}>New Session</button>
      <button onClick={loadHistory}>Load History</button>

      {result !== null && <h2>Result: {result}</h2>}

      <ul>
        {history.map((h, i) => (
          <li key={i}>
            {h.operandA} {h.operator} {h.operandB} = {h.result}
          </li>
        ))}
      </ul>
    </div>
  );
}
export default App;