import React, { useState, useEffect } from 'react';
import axios from 'axios';
import { Container, Table, TableBody, TableCell, TableContainer, TableHead, TableRow, Paper, Typography } from '@mui/material';

const BillsSummary = () => {
  const [bills, setBills] = useState([]);

  useEffect(() => {
    const fetchData = async () => {
      try {
        const response = await axios.get('https://localhost:5001/api/BillSummary');
        setBills(response.data);
      } catch (error) {
        console.error("There was an error fetching the bills data:", error);
      }
    };

    fetchData();
  }, []);

  return (
    <Container maxWidth="lg" component="main">
      <Typography component="h1" variant="h4" align="center" color="textPrimary" gutterBottom>
        Bills Summary
      </Typography>
      <TableContainer component={Paper}>
        <Table aria-label="Bills Summary Table">
          <TableHead>
            <TableRow>
              <TableCell>ID</TableCell>
              <TableCell>Bill</TableCell>
              <TableCell>Supporters</TableCell>
              <TableCell>Opposers</TableCell>
              <TableCell>Primary Sponsor</TableCell>
            </TableRow>
          </TableHead>
          <TableBody>
            {bills.map((bill) => (
              <TableRow key={bill.id}>
                <TableCell>{bill.id}</TableCell>
                <TableCell>{bill.bill}</TableCell>
                <TableCell>{bill.supporters}</TableCell>
                <TableCell>{bill.opposers}</TableCell>
                <TableCell>{bill.primarySponsor}</TableCell>
              </TableRow>
            ))}
          </TableBody>
        </Table>
      </TableContainer>
    </Container>
  );
};

export default BillsSummary;
