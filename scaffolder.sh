#!/bin/zsh

options=(
  "Option 1"
  "Option 2"
  "Option 3"
  "Exit"
)
selected=0

# Hide cursor
tput civis

function print_menu() {
    clear
    echo "Use ↑ ↓ to navigate, Enter to select:"
    for ((i = 0; i < ${#options[@]}; i++)); do
        if [[ $i -eq $selected ]]; then
            echo -e "> \e[1;32m${options[i]}\e[0m"
        else
            echo "  ${options[i]}"
        fi
    done
}

while true; do
    print_menu

    # Read one character (zsh-style)
    read -k1 key

    if [[ $key == $'\x1b' ]]; then
        read -k2 key  # read the next two characters (arrow key)
        case $key in
            '[A')  # Up arrow
                ((selected--))
                ((selected < 0)) && selected=$((${#options[@]} - 1))
                ;;
            '[B')  # Down arrow
                ((selected++))
                ((selected >= ${#options[@]})) && selected=0
                ;;
        esac
    elif [[ $key == $'\n' ]]; then
        break
    fi
done

# Show cursor again
tput cnorm
clear
echo "You selected: ${options[$selected]}"

