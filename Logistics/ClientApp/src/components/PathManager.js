import * as React from "react";

export default class PathManager extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            paths: props.paths || [],
            cities: props.cities || [],
            selectedFromId: 0,
            selectedToId: 0,
            selectedPathLength: 0,
            edittingFromId: 0,
            edittingToId: 0,
            edittingPathLength: 0,
            edittingPath: {},
        };
    }

    componentDidMount() {
        this.loadPaths();
    }

    render() {
        return (
            <div>
                <h3>Available paths</h3>
                <i>Click to update</i>
                {this.state.paths.map((p, index) => {
                    if (this.state.edittingPath.id === p.id) {
                        return this.showEditForm(this.state.edittingPath, index);
                    }
                    return <div onClick={() => this.setState({ edittingPath: this.state.paths[index] })}><i>from</i> <strong>{p.from.name}</strong> <i>to</i> <strong>{p.to.name}</strong> &mdash; {p.length} km</div>
                })}
                <hr />
                <h3>Add Path</h3>
                <div>
                    <label>From</label>
                    <select onChange={(e) => this.setState({ selectedFromId: e.target.value })}>
                        {this.state.cities.map(c => <option value={c.id}>{c.name}</option>)}
                    </select>

                    <label>To</label>
                    <select onChange={(e) => this.setState({ selectedToId: e.target.value })}>
                        {this.state.cities.map(c => <option value={c.id}>{c.name}</option>)}
                    </select>

                    <label>Length</label>
                    <input type="number" onChange={(e) => this.setState({ selectedPathLength: e.target.value })} />
                    <input type="button" disabled={this.state.selectedFromId === this.state.selectedToId} onClick={this.addPath.bind(this)} value="Add Path" />
                </div>
            </div>
        );
    }

    async loadPaths() {
        var response = await fetch("api/paths/all");
        var paths = await response.json();
        var cities = await (await fetch("api/cities/all")).json();
        console.dir(cities);
        this.setState({ paths, cities, edittingPath: {} });
    }

    async addPath() {
        const body = JSON.stringify({ fromCityId: parseInt(this.state.selectedFromId), toCityId: parseInt(this.state.selectedToId), selectedPathLength: Number(this.state.selectedPathLength) });
        const headers = {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        };
        const request = new Request("api/paths/add", { method: 'POST', body, headers });
        await fetch(request);
        await this.loadPaths();
    }

    async updatePath() {
        var path = this.state.edittingPath;
        var id = parseInt(path.id);
        var fromId = parseInt(path.from.id);
        var toId = parseInt(path.to.id);
        var length = Number(path.length);
        const body = JSON.stringify({ id, fromId, toId, length });
        const headers = {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        };
        const request = new Request("api/paths/update", { method: 'POST', body, headers });
        await fetch(request);
        await this.loadPaths();
    }

    showEditForm(path, index) {
        var { edittingPath } = this.state;
        return (
            <div key={index}>
                <i>From</i>
                <select onChange={(e) => {
                    edittingPath.from.id = parseInt(e.target.value);
                    this.setState({ edittingPath });
                }}>
                    {this.state.cities.map(c => <option selected={c.name == path.from.name} value={c.id}>{c.name}</option>)}
                </select>

                <label>To</label>
                <select onChange={(e) => {
                    edittingPath.to.id = parseInt(e.target.value);
                    this.setState({ edittingPath });
                }}>
                    {this.state.cities.map(c => <option selected={c.name == path.to.name} value={c.id}>{c.name}</option>)}
                </select>

                <label>Length</label>
                <input type="number" onChange={(e) => {
                    edittingPath.length = Number(e.target.value);
                    this.setState({ edittingPath });
                }} defaultValue={path.length} value={this.state.edittingPath.length} />
                <input type="button" disabled={edittingPath.from.id == edittingPath.to.id} onClick={this.updatePath.bind(this)} value="Update" />
            </div>
        );
    }
}