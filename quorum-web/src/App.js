import React from 'react';
import { BrowserRouter as Router, Route, Routes, Link } from 'react-router-dom';
import { Button, Container } from '@mui/material';
import LegislatorSummary from './LegislatorSummary'; 
import BillsSummary from './BillsSummary'; 

function App() {
  return (
    <Router>
      <Container maxWidth="sm">
        <Link to="/legislators-summary" style={{ textDecoration: 'none', margin: '20px 0' }}>
          <Button variant="contained" color="primary">
            See Legislators Summary
          </Button>
        </Link>
        <Link to="/bills-summary" style={{ textDecoration: 'none', margin: '20px 0' }}>
          <Button variant="contained" color="primary">
            See Bills Summary
          </Button>
        </Link>
        <Routes>
          <Route path="/legislators-summary" element={<LegislatorSummary />} />
        </Routes>
        <Routes>
          <Route path="/bills-summary" element={<BillsSummary />} />
        </Routes>

      </Container>
    </Router>
  );
}

export default App;
