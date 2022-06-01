import logo from './logo.svg';
import './App.css';
import MeyerNavbar from './components/Navbar';
import LogTable from './components/LogTable';
import { Container } from 'reactstrap';

function App() {
  return (
    <div className="App">
      <MeyerNavbar />
      <Container>
        <LogTable />
      </Container>
    </div>
  );
}

export default App;
