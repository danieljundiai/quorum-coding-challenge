import React, { useState, useEffect } from 'react';
import axios from 'axios';
import { Container, Table, TableBody, TableCell, TableContainer, TableHead, TableRow, Paper, Typography } from '@mui/material';

const LegislatorSummary = () => {
  const [legislators, setLegislators] = useState([]);

  useEffect(() => {
    const fetchData = async () => {
      try {
        const response = await axios.get('https://localhost:5001/api/LegislatorsSummary');
        setLegislators(response.data);
      } catch (error) {
        console.error("There was an error fetching the legislators data:", error);
      }
    };

    fetchData();
  }, []);

  return (
    <Container maxWidth="md" component="main">
      <Typography component="h1" variant="h4" align="center" color="textPrimary" gutterBottom>
        Legislators Summary
      </Typography>
      <TableContainer component={Paper}>
        <Table aria-label="Legislators Summary Table">
          <TableHead>
            <TableRow>
              <TableCell>ID</TableCell>
              <TableCell align="right">Legislator</TableCell>
              <TableCell align="right">Supported Bills</TableCell>
              <TableCell align="right">Opposed Bills</TableCell>
            </TableRow>
          </TableHead>
          <TableBody>
            {legislators.map((row) => (
              <TableRow key={row.id}>
                <TableCell component="th" scope="row">
                  {row.id}
                </TableCell>
                <TableCell align="right">{row.legislator}</TableCell>
                <TableCell align="right">{row.supportedBills}</TableCell>
                <TableCell align="right">{row.opposedBills}</TableCell>
              </TableRow>
            ))}
          </TableBody>
        </Table>
      </TableContainer>
    </Container>
  );
};

export default LegislatorSummary;
