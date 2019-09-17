import React from 'react';
import { Route } from 'react-router';
import Layout from './components/Layout';
import Home from './components/Home';
import Counter from './components/Counter';
import FetchData from './components/FetchData';
import Coins from './components/coins';
import Arbitrage from './components/arbitrage';

export default () => (
  <Layout>
    <Route exact path='/' component={Home} />
    <Route path='/coins' component={Coins} />
    <Route path='/arbitrage' component={Arbitrage} />
    <Route path='/counter' component={Counter} />
    <Route path='/fetch-data/:startDateIndex?' component={FetchData} />
  </Layout>
);
