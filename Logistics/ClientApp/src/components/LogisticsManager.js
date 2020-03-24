import * as React from "react";

export default class LogisticsManager extends React.Component {

    constructor(props) {
        super(props);
        this.state = {
            currentLogisticCenterCity: null
        };
    }

    componentDidMount() {
        this.loadCurrentLogisticCenterLocation();
    }

    render() {
        return (
            <div>
                <div>
                    <div>Current logistics center location</div>
                    <div><strong>{this.state.currentLogisticCenterCity || "No logistic center"}</strong></div>
                </div>
                <input type="button" onClick={this.setLogisticCenterCity.bind(this)} value="Set logistic center" />
            </div>
        );
    }

    async loadCurrentLogisticCenterLocation() {
        var response = await fetch("/api/logistics/getcurrent");
        var currentLogisticCenterCity = await response.text();
        this.setState({ currentLogisticCenterCity });
    }

    async setLogisticCenterCity() {
        const headers = {
            'Accept': 'text/plain',
            'Content-Type': 'application/json'
        };
        var response = await fetch("/api/logistics/set", {
            method: "POST",
            headers
        });
        var cityName = await response.text();
        if (cityName === this.state.currentLogisticCenterCity) {
            alert("Logistic center not modified");
        } else {
            this.setState({ currentLogisticCenterCity: cityName });
            alert("Logistic center changed to " + cityName);
        }
    }
}