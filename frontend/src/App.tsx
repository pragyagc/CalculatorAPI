import { useState, useEffect } from "react";
import { CalculatorService } from "./services/services/CalculatorService";
import "./Calculator.css";

function App() {
  const [display, setDisplay] = useState("");
  const [operandA, setOperandA] = useState("");
  const [operandB, setOperandB] = useState("");
  const [operation, setOperation] = useState<string | null>(null);
  const [currentOperand, setCurrentOperand] = useState<"A" | "B">("A");
  const [history, setHistory] = useState<any[]>([]); // no Calculation type

  useEffect(() => {
    loadHistory();
  }, []);

  const enterDigit = (digit: string) => {
    if (currentOperand === "A") {
      setOperandA(operandA + digit);
      setDisplay(operandA + digit);
    } else {
      setOperandB(operandB + digit);
      setDisplay(operandB + digit);
    }
  };

  const chooseOperation = (op: string) => {
    setOperation(op);
    setCurrentOperand("B");
    setDisplay(op);
  };

const calculate = async () => {
  
  
  const a = parseFloat(operandA);
  const b = parseFloat(operandB);

  // Must match CalculationRequest exactly
  const requestBody = {
    a: a,
    b: b,
    operation: operation!   // ✅ not "operator"
  };

  

  try {
  
    const response: any = await CalculatorService.postApiCalculatorCalculate(requestBody);
    
    setDisplay(response);
    // setOperandA(response.result.toString());
    // setOperandB("");
    // setOperation(null);
    // setCurrentOperand("A");

    loadHistory();
  } catch (err) {
    setDisplay("Error: " + (err as Error).message);
  }
};


  const clearAll = () => {
    setOperandA("");
    setOperandB("");
    setOperation(null);
    setDisplay("");
    setCurrentOperand("A");
  };

  const loadHistory = async () => {
    try {
      const data: any[] = await CalculatorService.getApiCalculatorHistory();
      setHistory(data);
    } catch (err) {
      console.error("Failed to load history", err);
    }
  };

  return (
    <div style={{ textAlign: "center", marginTop: "30px" }}>
      <h2>React Calculator</h2>
      <div className="calculator">
        <input className="display" value={display} readOnly />

        <div className="buttons">
          {[1,2,3,4,5,6,7,8,9,0].map(d => (
            <button key={d} onClick={() => enterDigit(d.toString())}>{d}</button>
          ))}
          <button onClick={() => chooseOperation("+")}>+</button>
          <button onClick={() => chooseOperation("-")}>-</button>
          <button onClick={() => chooseOperation("*")}>*</button>
          <button onClick={() => chooseOperation("/")}>/</button>
          <button onClick={calculate}>=</button>
          <button onClick={clearAll}>C</button>
        </div>
      </div>

      <h3>History</h3>
      <ul style={{ listStyle: "none", padding: 0 }}>
        {history.map((calc, index) => (
          <li key={index}>
            {calc.operandA} {calc.operator} {calc.operandB} = {calc.result}
          </li>
        ))}
      </ul>
    </div>
  );
}

export default App;
