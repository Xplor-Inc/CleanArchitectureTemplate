import { useState } from "react";

const AutoCompleteText = ({ suggestions, inputValue, setInputValue,placeHolder }: {
    suggestions: string[]
    setInputValue: (e: string) => void
    inputValue?: string
    placeHolder?:string
}) => {
    const [filteredSuggestions, setFilteredSuggestions] = useState<string[]>([]);
    const [activeSuggestionIndex, setActiveSuggestionIndex] = useState(0);
    const [showSuggestions, setShowSuggestions] = useState(false);

    const onChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        const userInput = e.target.value;

        // Filter our suggestions that don't contain the user's input
        const unLinked = suggestions.filter(
            (suggestion) =>
                suggestion.toLowerCase().indexOf(userInput.toLowerCase()) > -1
        );

        setInputValue(e.target.value);
        setFilteredSuggestions(unLinked);
        setActiveSuggestionIndex(0);
        setShowSuggestions(true);
    };

    const onClick = (e: any) => {
        setFilteredSuggestions([]);
        setInputValue(e.target.innerHTML);
        setActiveSuggestionIndex(0);
        setShowSuggestions(false);
    };

    const onKeyDown = (e: React.KeyboardEvent<HTMLInputElement>) => {
        // User pressed the enter key        
        if (e.key === 'enter') {
            setInputValue(filteredSuggestions[activeSuggestionIndex]);
            setActiveSuggestionIndex(0);
            setShowSuggestions(false);
        }
        // User pressed the Tab key
        if (e.key === 'Tab') {
            debugger
            var isAvailable = filteredSuggestions.filter(x => x.toLowerCase().includes(e.currentTarget.value.toLowerCase())).length > 0; 
            if (isAvailable) {
                setInputValue(filteredSuggestions[activeSuggestionIndex]);
            }
            else
                setInputValue(e.currentTarget.value);
            setActiveSuggestionIndex(0);
            setShowSuggestions(false);
        }

        // User pressed the up arrow
        else if (e.key === 'ArrowUp') {
            if (activeSuggestionIndex === 0) {
                return;
            }

            setActiveSuggestionIndex(activeSuggestionIndex - 1);
        }

        // User pressed the down arrow
        else if (e.key === 'ArrowDown') {
            if (activeSuggestionIndex - 1 === filteredSuggestions.length) {
                return;
            }

            setActiveSuggestionIndex(activeSuggestionIndex + 1);
        }
    };

    const SuggestionsListComponent = () => {
        return filteredSuggestions.length ? (
            <ul className="suggestions">
                {filteredSuggestions.map((suggestion, index) => {
                    let className;

                    // Flag the active suggestion with a class
                    if (index === activeSuggestionIndex) {
                        className = "suggestion-active";
                    }

                    return (
                        <li className={className} key={suggestion} onClick={onClick}>
                            {suggestion}
                        </li>
                    );
                })}
            </ul>
        ) : (
            <div className="no-suggestions">
                <span role="img" aria-label="tear emoji">
                    😪
                </span>{" "}
                <em>Sorry no suggestions</em>
            </div>
        );
    };

    return (
        <>
            <input type="text" placeholder={ placeHolder ?? 'Type to search'} className="form-control" required
                onChange={onChange}
                onKeyDown={onKeyDown}
                value={inputValue} />
            {showSuggestions && inputValue && <SuggestionsListComponent />}
        </>
    );
};

export default AutoCompleteText;