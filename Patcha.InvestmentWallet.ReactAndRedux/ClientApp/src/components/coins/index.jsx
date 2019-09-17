import React, { Component } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { actionCreators } from '../../store/coins';

class Coins extends Component {
    componentDidMount() {
        // This method is called when the component is first added to the document
        this.ensureDataFetched();
    }

    //componentDidUpdate() {
    //    // This method is called when the route parameters change
    //    this.ensureDataFetched();
    //}

    ensureDataFetched() {
        this.props.requestCoins();
    }

    render() {
        return (
            <div>
                {renderCoinsTable(this.props)}
            </div>
        );
    }
}

function renderCoinsTable(props) {
    return (
        <div>
            <table className="table">
                <thead>
                    <tr>
                        <th>#</th>
                        <th>Moeda</th>
                        <th>Preço</th>
                        <th>1h</th>
                        <th>24 h</th>
                        <th>7 d</th>
                        <th>Volume 24h</th>
                    </tr>
                </thead>
                <tbody>
                    {props.coins.map((coin, i) =>
                        <tr>
                            <td>{i+1}</td>
                            <td>
                                <img src="{coin.image.thumb}" alt="" />
                                <span>{coin.name}</span>
                            </td>
                            <td>R$ {coin.market_data.current_price.brl}</td>
                            <td>{coin.market_data.price_change_percentage_1h_in_currency.brl}%</td>
                            <td>{coin.market_data.price_change_percentage_24h_in_currency.brl}%</td>
                            <td>{coin.market_data.price_change_percentage_7d_in_currency.brl}%</td>
                            <td>R$ {coin.market_data.market_cap.brl}</td>
                        </tr>
                    )}
                </tbody>
            </table>
        </div>
    );
}

export default connect(
    state => state.coins,
    dispatch => bindActionCreators(actionCreators, dispatch)
)(Coins);
