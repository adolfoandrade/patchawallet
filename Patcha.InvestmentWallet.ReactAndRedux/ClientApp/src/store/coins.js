const requestCoinsType = 'REQUEST_COINS';
const receiveCoinsType = 'RECEIVE_COINS';
const initialState = { coins: [], isLoading: false };

export const actionCreators = {
    requestCoins: () => async (dispatch) => {
        dispatch({ type: requestCoinsType });

        const url = `https://patchawallet.azurewebsites.net/api/coins`;
        const response = await fetch(url);
        const coins = await response.json();

        dispatch({ type: receiveCoinsType, coins });
    }
};

export const reducer = (state, action) => {
    state = state || initialState;

    if (action.type === requestCoinsType) {
        return {
            ...state,
            isLoading: true
        };
    }

    if (action.type === receiveCoinsType) {
        return {
            ...state,
            coins: action.coins,
            isLoading: false
        };
    }

    return state;
};
