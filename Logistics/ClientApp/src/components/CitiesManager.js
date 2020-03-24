import * as React from "react";

export default class CitiesManager extends React.Component {

    constructor(props) {
        super(props);
        this.state = {
            cities: props.cities || [],
            editCityName: "",
            newCityName: ""
        };
    }

    componentDidMount() {
        this.loadCities();
    }

    render() {
        return (
            <div>
                <h3>Available cities</h3>
                {this.state.cities.map(c => this.getForm(c))}
                <div>
                    <label>Name</label>
                    <input type="text" onChange={(e) => this.setState({ newCityName: e.target.value })} value={this.state.newCityName} />
                    <input type="button" disabled={!this.state.newCityName} onClick={this.addCity.bind(this)} value="Add City" />
                </div>
            </div>
        );
    }

    async loadCities() {
        var cities = await fetch("/api/cities/all");
        var citiesJson = await cities.json()
        this.setState({ cities: citiesJson });
    }

    async addCity() {
        var response = await fetch("api/cities/add", {
            method: "POST",
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({ cityName: this.state.newCityName })
        });
        var city = await response.json();
        if (city) {
            var { cities } = this.state;
            cities.push(city);
            this.setState({ cities, newCityName: "" });
        }
    }

    updateCity(a, b, c) {
        debugger;
    }

    getForm(city) {
        return (
            <div>
                <input type="text" defaultValue={city.name} value={this.state.editCityName.name} onChange={(e) => this.setState({ editCityName: e.target.value })} />
                <input type="button" value="Update" onClick={this.updateCity.bind(this, city.id, this.state.editCityName)} />
            </div>
        );
    }

    async updateCity(cityId, newName) {
        var response = await fetch("api/cities/update", {
            method: "POST",
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({id: cityId, newName})
        });
        this.loadCities();
    }
}