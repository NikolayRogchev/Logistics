import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';

import './custom.css'
import CitiesManager from './components/CitiesManager';
import PathManager from './components/PathManager';
import LogisticsManager from './components/LogisticsManager';

export default class App extends Component {
    static displayName = App.name;

    render() {
        return (
            <Layout>
                <Route exact path='/' component={LogisticsManager} />
                <Route path='/cities' component={CitiesManager} />
                <Route path='/paths' component={PathManager} />
            </Layout>
        );
    }
}
