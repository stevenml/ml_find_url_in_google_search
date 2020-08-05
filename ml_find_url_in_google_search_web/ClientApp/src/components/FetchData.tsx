import React, { Component } from 'react';
import { observer } from 'mobx-react';
import { observable, action } from 'mobx';
import axios from 'axios';

@observer
export class FetchData extends Component {
	@observable
	private searchTerm: string = 'online title search';
	@observable
	private occurrenceTerm: string = 'infotrack.com.au';
	@observable
	private loadingStatus: 'INIT' | 'LOADING' | 'DONE' = 'INIT';
	@observable
	private rankings: string[] = [];

	componentDidMount() {
	}

	render() {
		return (
			<div>
				<h1 id="header">Check SEO Result</h1>
				<p>This app is for searching a string on Google and check how many times another string that you want to check has occurred in the results</p>
				<div>
					<form>
						<div>
							<label>Please enter the string you want to search</label>
							<input
								value={this.searchTerm}
								onChange={this.onSearchTermChange}
							/>
						</div>
						<div>
							<label>Please enter the string you want to check in the search results</label>
							<input
								value={this.occurrenceTerm}
								onChange={this.onOccurrenceTermChange}
							/>
						</div>
					</form>
					<button onClick={this.onSerchClick}>Search</button>
					{this.loadingStatus === 'LOADING' ? <p>Loading...</p> : null}
					{this.loadingStatus !== 'DONE' ? null : (
						<>
							<p>the positions of the occurrences in google search results:</p>
							<ul>
								{this.rankings.map(r => (
									<li>{r}</li>
								))}
							</ul>
						</>
					)}
				</div>
			</div>
		);
	}

	@action
	private onSearchTermChange = (event: React.ChangeEvent<HTMLInputElement>) => {
		this.searchTerm = event.target.value;
	}

	@action
	private onOccurrenceTermChange = (event: React.ChangeEvent<HTMLInputElement>) => {
		this.occurrenceTerm = event.target.value;
	}

	@action
	private onSerchClick = () => {
		this.loadingStatus = 'LOADING';
		axios.get(`/googleSearch/getSEOOccurrencesResult?searchTerm=${this.searchTerm}&occurrenceTerm=${this.occurrenceTerm}`)
			.then(({ data }) => {
				this.searchTerm = '';
				this.occurrenceTerm = '';
				this.loadingStatus = 'DONE';
				this.rankings = data;
			}).catch(() => {
				this.loadingStatus = 'INIT';
			});
	}

	async populateWeatherData() {
		const response = await fetch('weatherforecast');
		const data = await response.json();
		this.setState({ forecasts: data, loading: false });
	}
}
